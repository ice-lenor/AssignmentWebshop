﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AssignmentWebshop.Models;
using AssignmentWebshop.ProductImport;
using System.Collections.Generic;
using AssignmentWebshop.ProductImport.ProductValidations;

namespace AssignmentWebshop.Controllers
{
    /// <summary>
    /// Controller: the set of http-endpoints for managing the products
    /// </summary>
    public class ProductsController : Controller
    {
        const int PageSize = 100;

        private ProductDBContext m_db = new ProductDBContext();

        // GET: Products
        // GET: Products/startIndex=5
        public ActionResult Index(int? startIndex)
        {
            if (startIndex == null) startIndex = 0;

            var products = m_db.Products.OrderBy(x => x.Id).Skip(startIndex.Value).Take(PageSize).ToList();
            int productsTotalCount = m_db.Products.Count();
            int currentPage = (int)(startIndex / PageSize) + 1;
            int pagesTotalCount = (productsTotalCount / PageSize) + 1;
            var productsPage = new ProductsPage(products, currentPage, pagesTotalCount, productsTotalCount);
            if (startIndex > 0)
                productsPage.PreviousPageProductIndex = startIndex - PageSize;

            if (startIndex + PageSize < productsTotalCount)
                productsPage.NextPageProductIndex = startIndex + PageSize;

            return View(productsPage);
        }

        // POST: Products/Create
        [HttpPost]
        public JsonResult Create(IEnumerable<ProductRaw> products)
        {
            if (ModelState.IsValid && products != null)
            {
                // convert all products to valid model objects ready to be saved

                m_db.Configuration.AutoDetectChangesEnabled = false;

                var dictionaryCache = new DictionaryCache(m_db);
                var productValidator = new ProductValidator();
                ProductRawToProductConverter converter = new ProductRawToProductConverter(dictionaryCache, productValidator);
                var successfulCount = 0;
                var failedCount = 0;
                foreach (var productRaw in products)
                {
                    Product parsedProduct = converter.Convert(productRaw);
                    if (parsedProduct != null)
                    {
                        m_db.Products.Add(parsedProduct);
                        ++successfulCount;
                    }
                    else
                    {
                        ++failedCount;
                    }
                }

                if (successfulCount > 0)
                {
                    // save products
                    m_db.ChangeTracker.DetectChanges();
                    m_db.SaveChanges();
                    return Json(new { successfulCount = successfulCount, failedCount = failedCount });
                }
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

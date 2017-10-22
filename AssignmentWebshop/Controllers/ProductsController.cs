using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AssignmentWebshop.Models;
using AssignmentWebshop.ProductImport;
using System.Collections.Generic;

namespace AssignmentWebshop.Controllers
{
    /// <summary>
    /// Controller: the set of http-endpoints for managing the products
    /// </summary>
    public class ProductsController : Controller
    {
        const int PageSize = 10;

        private ProductDBContext db = new ProductDBContext();

        // GET: Products
        // GET: Products/startIndex=5
        public ActionResult Index(int? startIndex)
        {
            if (startIndex == null) startIndex = 0;
            return View(db.Products.OrderBy(x => x.Id).Skip(startIndex.Value).Take(PageSize).ToList());
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(IEnumerable<ProductRaw> products)
        {
            if (ModelState.IsValid)
            {
                ProductRawToProductConverter converter = new ProductRawToProductConverter(db);
                var atLeastOne = false;
                foreach (var productRaw in products)
                {
                    Product parsedProduct = converter.Convert(productRaw);
                    if (parsedProduct != null)
                    {
                        db.Products.Add(parsedProduct);
                        atLeastOne = true;
                    }
                }

                if (atLeastOne)
                {
                    db.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

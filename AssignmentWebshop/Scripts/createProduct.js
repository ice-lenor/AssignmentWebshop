var all_parsed = false;
var all_products = 0
var created_products = 0

function createProduct(csvRow) {

    console.log(csvRow)

    var product = {};

    product.ProductName = csvRow.Key;
    product.ArticleCode = csvRow.Artikelcode;
    product.ProductType = csvRow.colorcode;
    product.Manufacturer = csvRow.description;
    product.Price = csvRow.price;
    product.discountprice = csvRow.discountprice;
    product.DeliveryRange = csvRow["delivered in"];
    product.PersonType = csvRow.q1;
    product.Size = csvRow.size;
    product.Color = csvRow.color;

    console.log(product);

    ++all_products;

    $( document ).ajaxComplete(function () {
        console.log("ajaxComplete");
        ++created_products;

        if (all_parsed && created_products == all_products) {
            alert("The file has been successfully imported");
            location.reload();
        }
    });

    $.ajax({
        type: "POST",
        url: '/Products/Create',
        data: product, // Maps the controller params
        dataType: "json"
    });
}
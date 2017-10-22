var products = []

function createProductFromCsvRow(csvRow) {
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

    return product;

}
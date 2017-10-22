var all_parsed = false;
var all_products = 0;
var created_products = 0;

function parseProductsFile(file) {
    all_parsed = false;
    all_products = 0;
    created_products = 0;

    Papa.parse(file, {
        //worker: true, // workers don't work in local chrome?.. see https://stackoverflow.com/questions/21408510/chrome-cant-load-web-worker
        header: true,
        step: function (row) {
            if (row && row.data && row.data.length > 0) {
                createProduct(row.data["0"]);
            }
        },

        complete: function () {
            all_parsed = true;

            if (all_products == 0) {
                onAllImportFailure();
            }
        }
    });
}

function onAllImportSuccess() {
    alert("The products from the file have been successfully imported");
    location.reload();
}

function onAllImportFailure() {
    alert("The file is invalid or empty, please try again");
    location.reload();
}
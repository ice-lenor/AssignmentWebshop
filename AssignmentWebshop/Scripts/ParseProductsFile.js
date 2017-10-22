var all_parsed = false;
var all_requests = 0;
var finished_requests = 0;
var products = []

const bulk_post_size = 100;

// Parses the csv-file with products,
// sends batch post-requests to the backend to create the products,
// and shows the message to the user: success / failure.
function parseProductsFile(file) {
    all_parsed = false;
    all_requests = 0;
    finished_requests = 0;
    products = []

    // when a post request is finished, this is being executed
    $(document).ajaxComplete(function () {
        ++finished_requests;

        // if the file is fully parsed, and this is the last batch, show the "success" message
        if (all_parsed && finished_requests >= all_requests) {
            onAllImportSuccess();
        }
    });

    // parse the csv-file with products
    Papa.parse(file, {
        //worker: true, // workers don't work in local chrome?.. see https://stackoverflow.com/questions/21408510/chrome-cant-load-web-worker
        header: true,
        step: function (row) {
            parseOneRow(row);
        },
        complete: function () {
            onCsvParsingComplete();
        }
    });
}

// Parses one row of csv-file.
// If there's enough rows parsed for a batch create request, fires it.
function parseOneRow(row) {
    if (row && row.data && row.data.length > 0) {
        if (row.errors && row.errors.length > 0) {
            // This row is invalid. Because it is a "test exercise", we'll just log the error.
            // In a real production system, we could, for example:
            // - log an error and fire an alert for the user
            // - log to a special table "parsing problems" and offer to the user to fix all problems afterwards
            // - or offer the user to fix the error right away;
            // - skip the whole entry
            // - or save the whole entry without the errorneous field.
            // Please see a similar comment in ProductImport\ProductRawToProductConverter.cs.
            console.log("Csv row error", row.errors);
        } else {
            var product = createProductFromCsvRow(row.data["0"]);
            products.push(product);

            if (products.length >= bulk_post_size) {
                createProductsBatch();
            }
        }
    }
}

// Called when the parsing of the csv-file is completed.
// If there are still records in the last barch, sends a create request for it.
function onCsvParsingComplete() {
    all_parsed = true;

    // send the last batch for creation
    if (products.length > 0) {
        createProductsBatch();
    }

    // if the file is parsed, but there were no products, show the error message
    if (all_requests == 0) {
        onAllImportFailure();
    }
}

// Sends a post-request to the backend to create products.
function createProductsBatch() {
    var products_to_post_now = products.slice();
    products = []

    ++all_requests;

    $.ajax({
        type: "POST",
        url: '/Products/Create',
        data: { products: products_to_post_now },
        dataType: "json"
    });
}

// Show an "import succeeded" message
function onAllImportSuccess() {
    alert("The products from the file have been successfully imported");
    location.reload();
}

// Show an "import failed" message
function onAllImportFailure() {
    alert("Sorry, couldn't import products from the file.\nIt is possible the file is invalid or empty.");
    location.reload();
}

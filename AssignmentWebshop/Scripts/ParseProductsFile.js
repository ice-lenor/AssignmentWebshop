
function parseProductsFile(file) {
    Papa.parse(file, {
        //worker: true, // workers don't work in local chrome?.. see https://stackoverflow.com/questions/21408510/chrome-cant-load-web-worker
        header: true,
        step: function (row) {
            if (row && row.data && row.data.length > 0) {
                console.log("Row:", row.data["0"]);
                createProduct(row.data["0"]);
            }
        },

        complete: function() {
            console.log("All done!");
            alert("The file has been imported");
        }
    });

}
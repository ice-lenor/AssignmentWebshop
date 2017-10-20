
function parseProductsFile(file) {
    Papa.parse(file, {
        //worker: true, // workers don't work in local chrome?.. see https://stackoverflow.com/questions/21408510/chrome-cant-load-web-worker

        step: function(row) {
            console.log("Row:", row.data);
        },

        complete: function() {
            console.log("All done!");
        }
    });

}
This is the ASP.NET MVC application which lets the user to upload a .csv-file containing products, in a format which has been provided, and allows to see the imported records.

If you run it from the Visual Studio, it will create a webserver on localhost, and will open a browser page looking at that server.
For example, for me this address opens:
http://localhost:63605/
Also this endpoint would work as well:
http://localhost:63605/Products

This is the page which lets you upload the .csv-file (see the button on top), as well as look through all previously uploaded products (use buttons "Next page" / "Previous page").
See Screenshot1.png for the reference of how the page is supposed to look.

---

Please see the implemented frontend for uploading .csv-files here:
AssignmentWebshop\Scripts\parseProductsFile.js
This js-script parses the file line by line, doesn't load the whole file in memory and doesn't pass the whole file to the server, which allows the user to upload bigger files.
Then it sends the parsed products to the server in batches via the REST API.
When everything is uploaded, it displays a message to the user, telling them how many products it has uploaded successfully and how many - failed.

The REST API on server side is here:
AssignmentWebshop\Controllers\ProductsController.cs
On getting a batch of products, it validates them and then creates them in the database.
While doing so, it ensures first that all dictionary entries are in place, and creates them if needed. For example, if a product has a green color, it first creates the "Name='green', Id=123" entry in the Colors table, if there were no previously created green color; and then saves the product with a reference to that color.

The database is a Microsoft SQL Server Express instance, which is being created on start of the application automatically. It is unsuitable for a real production usage; but for testing purposes it's fine.
The schema is the following:
- The main table Products, with an autoincrement Id field, nvarchar-fields for product name and article code; money fields for price and discount price; and foreign keys on tables with colors, sizes, person types, manufacturers, etc.
- The additional dictionary tables with an autoincrement Id and an nvarchar unique Name fields.
The DB schema is mapped to the code via Entity Framework.

---

If I had a real client, I would be able to collect requirements from them. If I had a full specification, it would be even better. But instead I am going to make some assumptions which I am going to list here.

The naming of fields in csv-file looks shady: in the "colorcode" column, for example, there is generally a name of a piece of clothing (i.e., "broek" or "jacket"). I did my best making assumptions from the data provided; but of course, a specification would help greatly.

In .csv there are some invalid rows: more columns than in the header, etc. For now I skip these entries altogether.

According to my implementation, if the csv-format changes, for example, some columns are renamed, - these columns won't be uploaded. On the other hand, the column order doesn't matter.

I create dictionary entries during the upload of products - because I don't have a full list of dictionary values provided. If I had a fixed predefined list, I would be able to validate products instead: I could check that the uploaded product has a valid dictionary reference.

Speaking of validations. In a real production system there would be data validations in place; that's why there is this mechanism:
AssignmentWebshop\ProductImport\ProductValidations
I implemented a few validations as an example. Of course, in a real project there would be more of them; and their logic would be more complex: for example, they might check the database for duplicated article codes.

Since it is a small assignment, I skipped printing the results of validations for the user. Please check the comment in here:
AssignmentWebshop\ProductImport\ProductRawToProductConverter.cs
For now I just display how many records we failed to create.

There is no localization of strings in user interface.

The uploaded dictionary entries are not multilingual; which means that if we upload two entries with "groen" and "green" colors, there will be two separate color entries.

I didn't have time to write any tests. But if I had, I would use a mocking framework, for example, RhinoMocks, which would help me make mocks for interfaces I created. This way, I would be able to unit-test ProductRawToProductConverter, the whole validation mechanism (individual validations and the whole ProductValidator), and perhaps after some tweaks - the DictionaryCache.
A real production system could ask for integration tests: we could write some automated tests for the REST api; and perhaps UI tests as well.

I didn't have time to profile the application. The requirements didn't state the acceptable speed and memory consumption. It will work, but perhaps not as fast as it could have been.

Finally, please excuse some bootstrap code: it is remnants of the default MVC-project which Visual Studio creates by default.

---

Overall, please let me note that this task is, while not a huge one, certainly is not a tiny one as well. It takes quite some time to set up a project, especially for a person who's never worked with ASP.NET MVC before. Perhaps, if the purpose is not to see specifically MVC-related skills, it would help to ask for any UI-implementation; or for a smaller project.
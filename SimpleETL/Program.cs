using Microsoft.Extensions.Configuration;
using SimpleETL;
using SimpleETL.DataAccess;
using SimpleETL.DataExtraction;
using SimpleETL.DataProcessing;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string? csvFilePath = configuration["FilePaths:CsvFilePath"];
if (string.IsNullOrEmpty(csvFilePath))
{
    Console.WriteLine("Please specify a CSV file path.");
    return;
}

string? duplicatesFilePath = configuration["FilePaths:DuplicatesFilePath"];
if (string.IsNullOrEmpty(duplicatesFilePath))
{
    Console.WriteLine("Please specify a duplicates CSV file path.");
    return;
}

string? connectionString = configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Please specify a connection string.");
    return;
}

var controller = new AppController(
    new TaxiTripReader(csvFilePath),
    new TaxiTripProcessor(),
    new BulkInsertService(connectionString),
    new TaxiTripDuplicatesWriter(duplicatesFilePath));

try
{
    await controller.RunAsync();
    Console.WriteLine("Successfully completed");
}
catch (Exception e)
{
    Console.WriteLine("An error occurred while processing trips.");
    Console.WriteLine($"Exception: {e.Message}");
}

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SimpleETL.Mappings;
using SimpleETL.Models;

namespace SimpleETL.DataExtraction;

public class TaxiTripReader : ITaxiTripReader
{
    private readonly string _filePath;

    public TaxiTripReader(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<TaxiTrip>> ExtractDataAsync()
    {
        using var reader = new StreamReader(_filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) {        
            PrepareHeaderForMatch = args => args.Header.ToLower() });
        csv.Context.RegisterClassMap<TaxiTripMap>();

        var trips = new List<TaxiTrip>();
        await foreach (var record in csv.GetRecordsAsync<TaxiTrip>())
        {
            trips.Add(record);
        }
        
        return trips;
    }
}
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SimpleETL.Models;

namespace SimpleETL.DataProcessing;

public class TaxiTripDuplicatesWriter : ITaxiTripDuplicatesWriter
{
    private readonly string _filePath;

    public TaxiTripDuplicatesWriter(string filePath)
    {
        _filePath = filePath;
    }

    public async Task WriteDuplicatesAsync(List<TaxiTrip> trips)
    {
        await using var writer = new StreamWriter(_filePath);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
        await csv.WriteRecordsAsync(trips);
    }
}
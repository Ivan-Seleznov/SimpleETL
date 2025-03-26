using SimpleETL.DataAccess;
using SimpleETL.DataExtraction;
using SimpleETL.DataProcessing;
using SimpleETL.Models;

namespace SimpleETL;

public class AppController
{
    private readonly ITaxiTripReader _reader;
    private readonly ITaxiTripProcessor _processor;
    private readonly IBulkInsertService _bulkInsertService;
    private readonly ITaxiTripDuplicatesWriter _duplicateWriter;

    public AppController(ITaxiTripReader reader, ITaxiTripProcessor processor, IBulkInsertService bulkInsertService, ITaxiTripDuplicatesWriter duplicateWriter)
    {
        _reader = reader;
        _processor = processor;
        _bulkInsertService = bulkInsertService;
        _duplicateWriter = duplicateWriter;
    }

    public async Task RunAsync()
    {
        List<TaxiTrip> trips = await _reader.ExtractDataAsync();
        var result = _processor.ProcessTrips(trips);
        Console.WriteLine($"Unique trips count: {result.UniqueTrips.Count}; Duplicates count: {result.Duplicates.Count}");

        await _duplicateWriter.WriteDuplicatesAsync(result.Duplicates);
        await _bulkInsertService.BulkCopyTaxiTripsAsync(result.UniqueTrips);
    }
}
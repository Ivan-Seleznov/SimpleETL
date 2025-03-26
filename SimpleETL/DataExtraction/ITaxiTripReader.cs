using SimpleETL.Models;

namespace SimpleETL.DataExtraction;

public interface ITaxiTripReader
{ 
    Task<List<TaxiTrip>> ExtractDataAsync();
}
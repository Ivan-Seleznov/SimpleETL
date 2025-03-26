using SimpleETL.Models;

namespace SimpleETL.DataProcessing;

public interface ITaxiTripDuplicatesWriter 
{
    Task WriteDuplicatesAsync(List<TaxiTrip> trips);
}
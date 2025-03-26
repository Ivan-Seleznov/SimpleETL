using SimpleETL.Models;

namespace SimpleETL.DataProcessing;

public interface ITaxiTripProcessor
{
    TripProcessingResult ProcessTrips(List<TaxiTrip> trips);
}
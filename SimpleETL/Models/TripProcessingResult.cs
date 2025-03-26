namespace SimpleETL.Models;

public record TripProcessingResult(List<TaxiTrip> UniqueTrips, List<TaxiTrip> Duplicates)
{
    public List<TaxiTrip> UniqueTrips { get; set; } = UniqueTrips;
    public List<TaxiTrip> Duplicates { get; set; } = Duplicates;
}
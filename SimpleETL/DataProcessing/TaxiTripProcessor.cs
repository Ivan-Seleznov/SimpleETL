using SimpleETL.Models;

namespace SimpleETL.DataProcessing;

public class TaxiTripProcessor : ITaxiTripProcessor
{
    public TripProcessingResult ProcessTrips(List<TaxiTrip> trips)
    {
        var result = RemoveDuplicates(trips);
        var formattedTrips = result.UniqueTrips
            .Select(record =>
                {
                    record.PickupDateTime = ConvertToUtc(record.PickupDateTime);
                    record.DropoffDateTime = ConvertToUtc(record.DropoffDateTime);
                    record.StoreAndFwdFlag = record.StoreAndFwdFlag == "Y" ? "Yes" : "No";

                    return record;
                })
            .ToList();
        
        return new TripProcessingResult(formattedTrips, result.Duplicates);
    }
    private TripProcessingResult RemoveDuplicates(List<TaxiTrip> trips)
    {
        var groupedTrips = trips
            .GroupBy(record => new {
                PickupDateTime = record.PickupDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                DropoffDateTime = record.DropoffDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                PassengerCount = record.PassengerCount
            })
            .ToList();
        
        var duplicates = groupedTrips
            .Where(group => group.Count() > 1)
            .SelectMany(group => group.Skip(1))
            .ToList();

        var uniqueTrips = groupedTrips
            .Select(g => g.First())
            .ToList();
        
        return new TripProcessingResult(uniqueTrips, duplicates);
    }
    private DateTime ConvertToUtc(DateTime dateTime) => 
        TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
  
}
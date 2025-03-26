using SimpleETL.Models;

namespace SimpleETL.DataAccess;

public interface IBulkInsertService
{
    Task BulkCopyTaxiTripsAsync(List<TaxiTrip> trips);
}
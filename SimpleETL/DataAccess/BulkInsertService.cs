using Microsoft.Data.SqlClient;
using SimpleETL.Extensions;
using SimpleETL.Models;

namespace SimpleETL.DataAccess;

public class BulkInsertService : IBulkInsertService
{
    private const string TaxiTripsTableName =  "TaxiTrips";
    private readonly string _connectionString;

    public BulkInsertService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task BulkCopyTaxiTripsAsync(List<TaxiTrip> trips)
    {
        await using var connection = new SqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync();

            using var bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.DestinationTableName = TaxiTripsTableName;

            var table = trips.ToDataTable();

            await bulkCopy.WriteToServerAsync(table);
        }
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync(e.Message);
            throw;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}
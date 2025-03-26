# SimpleETL
Test Assesment for DevelopsToday

## Configuration
You can specify the paths and connection string in appsettings.json

## SQL
```sql
CREATE TABLE TaxiTrips (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PickupDateTime DATETIME NOT NULL,
    DropoffDateTime DATETIME NOT NULL,
    PassengerCount INT NULL,
    TripDistance FLOAT NOT NULL,
    StoreAndFwdFlag NVARCHAR(3) NOT NULL,
    PULocationID INT NOT NULL,
    DOLocationID INT NOT NULL,
    FareAmount DECIMAL(18, 2) NOT NULL,
    TipAmount DECIMAL(18, 2) NOT NULL
);

CREATE INDEX IX_PULocationID_TipAmount ON TaxiTrips (PULocationID, TipAmount);

CREATE INDEX IX_TripDistance ON TaxiTrips (TripDistance DESC);

CREATE INDEX IX_TripDuration ON TaxiTrips (DropoffDateTime DESC, PickupDateTime ASC);

CREATE INDEX IX_PULocationID ON TaxiTrips (PULocationID);
```

## If the program will be used on much larger data files
First, I would switch from loading the entire CSV file into memory to processing it in smaller chunks or using a streaming approach which can reduce memory usage.
Additionally, I would optimize the bulk insert operation by batching the records and inserting them in smaller chunks rather than all at once.
Lastly, use parallel processing where applicable to utilize multiple CPU cores for faster data processing.

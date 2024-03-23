namespace admete.MockedDatabase;

public class DatabaseService : IDatabaseService
{
    public IDatabaseClient GetClient(string name)
    {
        return name switch
        {
            "LiveCasinoMain" => new LiveCasinoMainDatabaseClient(),
            "SBO2024" => new Sbo2024DatabaseClient(),
            "SBO2023" => new Sbo2023DatabaseClient(),
            _ => throw new Exception("Invalid database name")
        };
    }
}

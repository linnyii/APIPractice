namespace admete.MockedDatabase;

public interface IDatabaseService
{
    IDatabaseClient GetClient(string name);
}
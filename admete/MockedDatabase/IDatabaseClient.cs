namespace admete.MockedDatabase;

public interface IDatabaseClient
{
    IEnumerable<GameResultDto> GetGameResults();
}

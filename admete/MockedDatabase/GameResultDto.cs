namespace admete.MockedDatabase;

public class GameResultDto
{
    public int ProductType { get; init; }
    public int GameId { get; init; }
    public string StationName { get; init; } = null!;
    public DateTime StartTime { get; init; }
    public string? GameDetail { get; init; }

    public override string ToString()
    {
        return $"{nameof(ProductType)}: {ProductType}, {nameof(GameId)}: {GameId}, {nameof(StationName)}: {StationName}, {nameof(StartTime)}: {StartTime}, {nameof(GameDetail)}: {GameDetail}";
    }
}

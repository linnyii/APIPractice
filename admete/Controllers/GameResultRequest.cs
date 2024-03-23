namespace admete.Controllers;

public class GameResultRequest
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int ProductType { get; set; }

    public bool InvalidStartDate()
    {
        return StartTime < new DateTime(2023, 1, 1);
    }

    public bool InvalidDate()
    {
        return StartTime >= EndTime;
    }
}
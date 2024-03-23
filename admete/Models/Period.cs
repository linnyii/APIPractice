namespace admete.Models;

public class Period
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public bool IsOverlapping(Period period)
    {
        return Start < period.End && End > period.Start;
    }
}
namespace BattleshipLiteLibrary;

public class GridSpot
{
    public string Row { get; set; } = string.Empty;
    public int Column { get; set; }
    public Status Status { get; set; } = Status.Empty;
}
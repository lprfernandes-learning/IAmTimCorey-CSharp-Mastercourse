namespace BattleshipLiteLibrary;

public class GridSpot
{
    public string GridSpotLetter { get; set; } = string.Empty;
    public int GridSpotNumber { get; set; }
    public GridSpotStatus GridSpotStatus { get; set; } = GridSpotStatus.Empty;
}
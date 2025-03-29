namespace BattleshipLiteLibrary;

public class PlayerInfo
{
    public string UsersName { get; set; } = string.Empty;
    public List<GridSpot> ShipLocations { get; set; } = new List<GridSpot>();
    public List<GridSpot> ShotLocations { get; set; } = new List<GridSpot>();
    public int NrShotsTaken { get; set; }
}

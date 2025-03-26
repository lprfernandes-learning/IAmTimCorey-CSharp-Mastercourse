namespace BattleshipLiteLibrary;

public class PlayerInfo
{
    public string Username { get; set; } = string.Empty;
    public GridSpot[] ShipLocations { get; set; } = new GridSpot[5];
    public GridSpot[] ShotLocations { get; set; } = new GridSpot[5];

}

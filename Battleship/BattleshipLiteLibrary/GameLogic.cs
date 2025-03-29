
namespace BattleshipLiteLibrary;

public static class GameLogic
{
    public static readonly string[] GridLetters = { "A", "B", "C", "D", "E" };

    public static bool IdentifyShotResult(PlayerInfo opponent, string row, int column)
    {
        (bool output, GridSpot? spot) = ValidateGridSpotStatus(opponent.ShipLocations, row, column, Status.Ship);

        if (output && spot != null)
        {
            spot.Status = Status.Sunk;
        }
        return output;
    }

    public static void InitializeGrid(PlayerInfo model)
    {
        for (int i = 0; i < GridLetters.Length; i++)
        {
            for (int j = 0; j < GridLetters.Length; j++)
            {
                GridSpot spot1 = new GridSpot()
                {
                    Row = GridLetters[i],
                    Column = j + 1
                };
                GridSpot spot2 = new GridSpot()
                {
                    Row = GridLetters[i],
                    Column = j + 1
                };
                model.ShipLocations.Add(spot1);
                model.ShotLocations.Add(spot2);
            }
        }
    }

    public static void RecordShotResult(PlayerInfo player, string row, int column, bool isAHit)
    {
        GridSpot spot = GetGridSpot(player.ShotLocations, row, column)!;

        if (isAHit)
        {
            spot.Status = Status.Hit;
        }
        else
        {
            spot.Status = Status.Miss;
        }
    }

    public static bool PlaceShip(PlayerInfo model, string location)
    {
        (string row, int column) = SplitShotIntoRowAndColumn(location);

        (bool isSpotOpen, GridSpot? spot) = ValidateGridSpotStatus(model.ShipLocations, row, column, Status.Empty);

        if (isSpotOpen)
        {
            spot.Status = Status.Ship;
            return true;
        }
        return false;
    }

    private static bool ValidateGridLocation(string row, int column)
    {
        return GridLetters.Contains(row.ToUpper()) && column <= Math.Pow(GridLetters.Length, 2);
    }

    public static bool PlayerDefeated(PlayerInfo opponent)
    {
        return !opponent.ShipLocations.Any(x => x.Status == Status.Ship);
    }

    public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
    {
        if (shot.Length != 2)
        {
            throw new ArgumentException("This was an invalid shot type.", shot);
        }

        string row = shot.Substring(0, 1);
        string strColumn = shot.Substring(1);

        if (!int.TryParse(strColumn, out int column))
        {
            throw new ArgumentException("Column value is not correct", shot);
        }
        return (row, column);

    }

    public static bool ValidateShot(PlayerInfo activePlayer, string row, int column)
    {
        (bool output, _) = ValidateGridSpotStatus(activePlayer.ShotLocations, row, column, Status.Empty);
        return output;
    }

    private static (bool, GridSpot?) ValidateGridSpotStatus(IEnumerable<GridSpot> locations, string row, int column, Status status)
    {
        GridSpot? spot = GetGridSpot(locations, row, column);

        if (spot?.Status == status)
        {
            return (true, spot);
        }
        return (false, spot);
    }

    private static GridSpot? GetGridSpot(IEnumerable<GridSpot> locations, string row, int column)
    {
        if (ValidateGridLocation(row, column))
        {
            return locations.SingleOrDefault(x => x.Column == column && x.Row == row.ToUpper());
        }
        return null;
    }
}

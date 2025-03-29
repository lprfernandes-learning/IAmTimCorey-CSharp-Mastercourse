
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using BattleshipLiteLibrary;


internal class Program
{
    static void Main(string[] args)
    {
        WelcomeMessage();
        PlayerInfo activePlayer = CreatePlayer("Player 1");
        PlayerInfo opponent = CreatePlayer("Player 2");
        PlayerInfo winner = null;

        do
        {
            DisplayShotGrid(activePlayer);

            RecordPlayerShot(activePlayer, opponent);

            bool isGameOver = GameLogic.PlayerDefeated(opponent);

            if (isGameOver)
            {
                winner = activePlayer;
            }
            else
            {
                (activePlayer, opponent) = (opponent, activePlayer);
            }

        } while (winner == null);

        IdentifyWinner(winner);
        Console.ReadLine();
    }

    private static void IdentifyWinner(PlayerInfo winner)
    {
        Console.WriteLine($"Congratulations to {winner.UsersName} for winning");
        Console.WriteLine($"{winner.UsersName} took {winner.NrShotsTaken} shots.");

    }

    private static void RecordPlayerShot(PlayerInfo activePlayer, PlayerInfo opponent)
    {
        bool isValidShot = false;
        string row = string.Empty;
        int column = 0;
        do
        {
            string shot = AskForShot(activePlayer);

            try
            {
                (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
            }
            catch (Exception ex)
            {
                isValidShot = false;
            }

            if (isValidShot == false)
            {
                Console.WriteLine("Invalid shot location. Please try again.");
            }

        } while (isValidShot == false);

        // Determine shot results
        bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

        // Record results
        GameLogic.RecordShotResult(activePlayer, row, column, isAHit);

        DisplayShotResults(row, column, isAHit);

        activePlayer.NrShotsTaken++;
    }

    private static void DisplayShotResults(string row, int column, bool isAHit)
    {

        if (isAHit)
        {
            Console.WriteLine($"{row}{column} is a Hit");
        }
        else
        {
            Console.WriteLine($"{row}{column} is a Miss");
        }
        Console.WriteLine();
    }

    private static string AskForShot(PlayerInfo player)
    {
        Console.Write($"{player.UsersName}, please enter your shot selection: ");
        return Console.ReadLine();
    }

    private static void DisplayShotGrid(PlayerInfo activePlayer)
    {
        string currentRow = activePlayer.ShotLocations.First().Row;

        foreach (var spot in activePlayer.ShotLocations)
        {
            if (spot.Row != currentRow)
            {
                Console.WriteLine();
                currentRow = spot.Row;
            }
            if (spot.Status == Status.Empty)
            {
                Console.Write("[ ]");
            }
            else if (spot.Status == Status.Hit)
            {

                Console.Write(" X ");
            }
            else if (spot.Status == Status.Miss)
            {

                Console.Write(" O ");
            }
            else
            {
                Console.Write(" ? ");
            }
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    private static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to Battleship Lite");
        Console.WriteLine("Created by Tim Corey");
        Console.WriteLine();
    }

    private static PlayerInfo CreatePlayer(string playerTitle)
    {
        Console.WriteLine($"Player Information for {playerTitle}");

        PlayerInfo output = new PlayerInfo();

        //Ask the user for their name
        output.UsersName = AskForUsersName();

        //Load up the shot grid
        GameLogic.InitializeGrid(output);

        //Ask the user for their ship placements
        AskForShipLocations(output);

        Console.Clear();
        return output;
    }

    private static string AskForUsersName()
    {
        Console.Write("What is your name?");
        string output = Console.ReadLine();
        return output;
    }

    private static void AskForShipLocations(PlayerInfo model)
    {
        int placedShips = 0;
        do
        {

            Console.WriteLine($"Where do you want to place your ship number: {placedShips + 1}");
            string location = Console.ReadLine();

            bool isValidLocation = false;

            try
            {
                isValidLocation = GameLogic.PlaceShip(model, location);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isValidLocation = false;
            }

            if (isValidLocation == false)
            {
                Console.WriteLine("That was not a valid location. Please try again.");
            }

            placedShips = model.ShipLocations.Count(x => x.Status == Status.Ship);

        } while (placedShips < 5);
    }
}

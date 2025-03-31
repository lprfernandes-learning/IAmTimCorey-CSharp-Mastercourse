using System.Runtime.ConstrainedExecution;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        List<IRentable> rentables = new List<IRentable>();
        List<IPurchasable> purchasables = new List<IPurchasable>();

        var vehicle = new VehicleModel() { DealerFee = 25, ProductName = "Kia Optima" };
        var book = new BookModel() { ProductName = "A Tale of Two cities", NumberOfPages = 350 };
        var excavator = new ExcavatorModel() { ProductName = "Bulldozer", QuantityInStock = 2 };

        rentables.Add(vehicle);
        rentables.Add(excavator);

        purchasables.Add(book);
        purchasables.Add(vehicle);

        System.Console.Write("Do you want to rent or purchase: (rent/purchase)");
        string? rentalDecision = Console.ReadLine();

        if (rentalDecision?.ToLower() == "rent")
        {
            foreach (var item in rentables)
            {
                Console.WriteLine($"Item:{item.ProductName} ");
                Console.Write("Do you want to rent this item? (yes/no)");
                string wantToRent = Console.ReadLine();

                if (wantToRent?.ToLower() == "yes")
                {
                    item.Rent();
                }

                Console.WriteLine($"Item:{item.ProductName} ");
                Console.Write("Do you want to return this item? (yes/no)");
                string wantToReturn = Console.ReadLine();

                if (wantToReturn?.ToLower() == "yes")
                {
                    item.ReturnRental();
                }
            }
        }
        else
        {
            foreach (var item in purchasables)
            {
                Console.WriteLine($"Item:{item.ProductName} ");
                Console.Write("Do you want to purchase this item? (yes/no)");
                string wantToPurchase = Console.ReadLine();

                if (wantToPurchase?.ToLower() == "yes")
                {
                    item.Purchase();
                }
            }
        }
        Console.WriteLine("We are done");
        Console.ReadLine();
    }

}

public class InventoryItemModel : IInventoryItem
{
    public string ProductName { get; set; }
    public int QuantityInStock { get; set; }
}

public class VehicleModel : InventoryItemModel, IPurchasable, IRentable
{
    public decimal DealerFee { get; set; }

    public void Purchase()
    {
        QuantityInStock--;
        Console.WriteLine("This vehicle has been purchased");
    }

    public void Rent()
    {
        QuantityInStock--;
        Console.WriteLine("This vehicle has been rented");
    }

    public void ReturnRental()
    {
        QuantityInStock++;
        Console.WriteLine("This vehicle has been returned");
    }
}
public class BookModel : InventoryItemModel, IPurchasable
{
    public int NumberOfPages { get; internal set; }

    public void Purchase()
    {
        QuantityInStock--;
        Console.WriteLine("This book has been purchased");

    }
}
public class ExcavatorModel : InventoryItemModel, IRentable
{
    public void Rent()
    {
        QuantityInStock--;
        Console.WriteLine("This excavator has been rented");
    }

    public void ReturnRental()
    {
        QuantityInStock++;
        Console.WriteLine("This excavator has been returned");
    }
}

interface IRentable : IInventoryItem
{
    void Rent();
    void ReturnRental();
}
interface IPurchasable : IInventoryItem
{
    void Purchase();
}

interface IInventoryItem
{
    string ProductName { get; set; }
    int QuantityInStock { get; set; }
}
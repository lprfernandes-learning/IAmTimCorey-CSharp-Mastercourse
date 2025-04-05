class Program
{
    static void Main()
    {

        List<Person> people = new List<Person> {
        new Person("Tim", "Corey", "tim@iamtim@gmail.com"),
        new Person("Sue", "Storm", "sue@iamtim@gmail.com"),
        new Person("John", "Smith", "john@iamtim@gmail.com")};

        List<Car> cars = new List<Car> {
        new Car("Toyota", "Corolla"),
        new Car("ToyotaHeck", "Corolla2"),
        new Car("Toyota", "Highlander"),
        new Car("Ford", "Mustang")};

        DataAccess<Person> writerPeople = new();
        DataAccess<Car> writerCars = new();

        writerPeople.BadDataFound += BadDataFound;
        writerCars.BadDataFound += BadDataFound;

        writerPeople.SaveToCsv(people, @".\people.csv");
        writerCars.SaveToCsv(cars, @".\cars.csv");
    }

    private static void BadDataFound<T>(object sender, T e)
    {
        Console.WriteLine($"Bad entry on record: {e}");
    }


}

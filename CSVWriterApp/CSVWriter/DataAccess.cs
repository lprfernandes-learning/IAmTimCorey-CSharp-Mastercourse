
public class DataAccess<T>
{
    public event EventHandler<T> BadDataFound;


    public void SaveToCsv(IEnumerable<T> items, string filePath)
    {

        List<string> lines = new List<string>();

        var properties = typeof(T).GetProperties();
        var columns = properties.Select(x => x.Name);
        string headerRow = string.Join(',', columns);
        lines.Add(headerRow);

        foreach (var item in items)
        {
            var values = properties.Select(x => x.GetValue(item)?.ToString()).ToList();
            string row = string.Join(',', values);

            if (BadWordDetector(row) == false)
            {
                lines.Add(row);
            }
            else
            {
                OnFindingBadData(item);
            }
        }
        File.WriteAllLines(filePath, lines);
    }

    private void OnFindingBadData(T item)
    {
        BadDataFound?.Invoke(this, item);
    }

    private static bool BadWordDetector(string stringToTest)
    {
        string lowerCaseTest = stringToTest.ToLower();

        return lowerCaseTest.Contains("darn") || lowerCaseTest.Contains("heck");
    }
}
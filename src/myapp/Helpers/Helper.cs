namespace myapp.Helper;
public class Helper
{
    public static string[] GetInputLines(int day)
    {
        if (day < 1 || day > 25) throw new ArgumentOutOfRangeException(nameof(day));

        var inputFile = $"{1200 + day}.txt";

        return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Inputs", inputFile));
    }
}
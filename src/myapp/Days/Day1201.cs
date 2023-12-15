namespace myapp.Days;
public class Day1201 : PuzzleBase
{
    public Day1201()
    {
        Date = new DateOnly(2023, 12, 1);
        init();
    }

    private int GetLineFirstNumber(char[] lineChars)
    {
        if (lineChars == null || lineChars.Length == 0) return 0;

        var number = 0;
        foreach (var ch in lineChars)
        {
            if (int.TryParse(ch.ToString(), out number))
            {
                break;
            }
        }
        return number;
    }

    private static Dictionary<int, string> Numbers = new Dictionary<int, string>();
    private static string[] digitalArray = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    private void init()
    {
        var i = 1;
        foreach (var item in digitalArray)
        {
            Numbers[i] = digitalArray[i - 1];
            i++;
        }
    }

    private int GetLineFirstNumber(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return 0;

        line = line.ToLower();
        var firstNumber = GetLineFirstNumber(line.ToArray());
        var firstNumberIndex = line.IndexOf(firstNumber.ToString());

        var firstCharNumberIndex = -1;
        var charNumber = string.Empty;
        foreach (var num in digitalArray)
        {
            var index = line.IndexOf(num);
            if (index >= 0)
            {
                if (firstCharNumberIndex < 0 || index < firstCharNumberIndex)
                {
                    firstCharNumberIndex = index;
                    charNumber = num;
                }
            }
        }

        if (firstCharNumberIndex < 0 || firstNumberIndex < firstCharNumberIndex) return firstNumber;

        return Numbers.Single(x => x.Value == charNumber).Key;
    }

    private int GetLineLastNumber(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return 0;

        line = line.ToLower();
        var lastNumber = GetLineFirstNumber(line.Reverse().ToArray());
        var lastNumberIndex = line.LastIndexOf(lastNumber.ToString());

        var lastCharNumberIndex = -1;
        var charNumber = string.Empty;
        foreach (var num in digitalArray)
        {
            var index = line.LastIndexOf(num);
            if (index >= 0)
            {
                if (lastCharNumberIndex < 0 || lastCharNumberIndex < index)
                {
                    lastCharNumberIndex = index;
                    charNumber = num;
                }
            }
        }

        if (lastCharNumberIndex < 0 || lastCharNumberIndex < lastNumberIndex) return lastNumber;

        return Numbers.Single(x => x.Value == charNumber).Key;
    }

    public override string PartOne()
    {
        var sum = 0;
        foreach (var line in InputLines)
        {
            var firstNumber = GetLineFirstNumber(line.ToArray());
            var lastNumber = GetLineFirstNumber(line.Reverse().ToArray());

            sum += firstNumber * 10 + lastNumber;
        }

        return sum.ToString();
    }

    public override string PartTwo()
    {
        var sum = 0;
        foreach (var line in InputLines)
        {
            var firstNumber = GetLineFirstNumber(line);
            var lastNumber = GetLineLastNumber(line);

            sum += firstNumber * 10 + lastNumber;
        }

        return sum.ToString();
    }
}
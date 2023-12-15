using System.Data;

namespace myapp;
public class Day1215 : PuzzleBase
{
    public Day1215()
    {
        Date = new DateOnly(2023, 12, 15);
    }

    public override string PartOne()
    {
        long sum = 0;
        var infos = InputLines[0].Split(",", StringSplitOptions.RemoveEmptyEntries)
                                 .Select(x => new HashInfo { Chars = x }).ToList();

        foreach (var item in infos)
        {
            var lastValue = 0;
            for (int i = 0; i < item.Chars.Length; i++)
            {
                var value = item.Chars[i] + lastValue;
                item.Result = value * 17 % 256;
                lastValue = item.Result;
            }
        }

        return infos.Sum(x => x.Result).ToString();
    }


    // not complete
    public override string PartTwo()
    {
        return "";
    }
}

public class HashInfo
{
    public string Chars { get; set; }
    public int Result { get; set; }
}
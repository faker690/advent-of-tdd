using System.Text.RegularExpressions;

namespace myapp;
public class Day1203 : PuzzleBase
{
    public Day1203()
    {
        Date = new DateOnly(2023, 12, 3);
    }

    private void execute()
    {
        // arry for the inputs
        var picArray = new char[140, 140];
        for(var r = 0; r < InputLines.Count(); r++){
            for(var j = 0; j< InputLines[r].Length; j++){
                picArray[r, j] = InputLines[r][j];
            }
        }

        // find the number and symbol
        for (int i = 0; i < InputLines.Length; i++)
        {
            var line = InputLines[i];
            var numbers = Regex.Matches(line, "\\d+");
            line.IndexOfAny(,);

            for (int j = 0; j < picArray.GetLength(1); j++)
            {
                if(picArray[i, j] == '.') continue;

                int.TryParse(picArray[i, j].ToString(), out int num);
            }
        }

        // check the number is adjacent to a symbol
    }

    public override string GetResult(int PuzzleOrder)
    {
        var results = execute();
        if (PuzzleOrder == 1)
            return results.Where(x => x.Games.All(y => y.IsValid)).Select(x => x.Index).Distinct().Sum(x => x).ToString();

        if (PuzzleOrder == 2)
        {
            var total = 0;
            results.ForEach(x =>
            {
                var maxRed = x.Games.Max(y => y.RedCube);
                var maxGreen = x.Games.Max(y => y.GreenCube);
                var maxBlue = x.Games.Max(y => y.BlueCube);

                var gameSum = Math.Max(1, maxRed) * Math.Max(1, maxGreen) * Math.Max(1, maxBlue);
                total += gameSum;
            });

            return total.ToString();
        }

        return "Invalid";
    }
}

public class EngineInfo
{
    public int Row { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
}

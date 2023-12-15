
namespace myapp.Days;
public class Day1204 : PuzzleBase
{
    public Day1204()
    {
        Date = new DateOnly(2023, 12, 4);
    }

    public override string PartOne()
    {
        var sum = 0;
        foreach (var line in InputLines)
        {
            var info = getCardInfo(line);
            if (info.CardWinNO > 0)
                sum += (int)Math.Pow(2, info.CardWinNO - 1);
        }

        return sum.ToString();
    }

    public override string PartTwo()
    {
        var infos = InputLines.Select(getCardInfo).OrderBy(x => x.CardIndex).ToList();
        for (int i = 0; i < infos.Count; i++)
        {
            var current = infos[i];
            for (int j = 0; j < current.CardWinNO; j++)
            {
                infos[i + j + 1].Count++;
            }
        }

        return infos.Select(x => x.Count).Sum().ToString();
    }

    private CardInfo getCardInfo(string line)
    {
        var cardNO = line.Split(":")[0];
        var data = line.Split(":")[1];
        var dataItems = data.Split("|");
        var firstDataList = dataItems[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var secondDataList = dataItems[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var winNo = secondDataList.Count(x => firstDataList.Any(y => x == y));

        return new CardInfo
        {
            CardNO = cardNO,
            CardData = data,
            CardIndex = int.Parse(cardNO.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]) - 1,
            CardWinNO = winNo,
            Count = 1
        };
    }
}

public class CardInfo
{
    public required string CardNO { get; set; }
    public required string CardData { get; set; }
    public int CardIndex { get; set; }
    public int CardWinNO { get; set; }

    public int Count { get; set; }
}


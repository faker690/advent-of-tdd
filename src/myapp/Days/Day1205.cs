using System.Collections.Concurrent;

namespace myapp.Days; public class Day1205 : PuzzleBase
{
    public Day1205()
    {
        Date = new DateOnly(2023, 12, 5);
    }

    public override string PartOne()
    {
        // get seeds
        var seeds = InputLines[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        var seedInfos = seeds.Select(x => new SeedInfo(long.Parse(x))).ToList();

        // get middleSet
        var soilInfos = getMiddleInfos(4, 28);
        var fertInfos = getMiddleInfos(31, 39);
        var waterInfos = getMiddleInfos(42, 74);
        var lightInfos = getMiddleInfos(77, 122);
        var tempInfos = getMiddleInfos(125, 159);
        var humInfos = getMiddleInfos(162, 181);
        var locInfos = getMiddleInfos(184, 226);

        // find all locations
        foreach (var info in seedInfos)
        {
            info.Soil = getResult(soilInfos, info.Seed);
            info.Fertilezer = getResult(fertInfos, info.Soil);
            info.Water = getResult(waterInfos, info.Fertilezer);
            info.Light = getResult(lightInfos, info.Water);
            info.Temperature = getResult(tempInfos, info.Light);
            info.Humidity = getResult(humInfos, info.Temperature);
            info.Location = getResult(locInfos, info.Humidity);
        }

        return seedInfos.Min(x => x.Location).ToString();
    }

    private long getResult(List<MiddleInfo> infos, long value)
    {
        var tmp = infos.FirstOrDefault(x => value >= x.SourceStartNo && value < (x.SourceStartNo + x.Range));
        if (tmp == null) return value;

        return tmp.DestStartNo - tmp.SourceStartNo + value;
    }

    private List<MiddleInfo> getMiddleInfos(int from, int to)
    {
        var infos = new List<MiddleInfo>();
        for (int i = from - 1; i < to; i++)
        {
            var items = InputLines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
            if (items.Count != 3) throw new Exception();

            infos.Add(new MiddleInfo
            {
                DestStartNo = items[0],
                SourceStartNo = items[1],
                Range = items[2]
            });
        }
        return infos;
    }

    // not complete
    public override string PartTwo()
    {
        return "";
    }

}

public class MiddleInfo
{
    public long DestStartNo { get; set; }
    public long SourceStartNo { get; set; }
    public long SourceEndNo => SourceStartNo + Range - 1;
    public long Range { get; set; }
}

public class SeedInfo
{
    public SeedInfo(long seed) => Seed = seed;

    public long Seed { get; init; }
    public long Soil { get; set; }
    public long Fertilezer { get; set; }
    public long Water { get; set; }
    public long Light { get; set; }
    public long Temperature { get; set; }
    public long Humidity { get; set; }
    public long Location { get; set; }
}

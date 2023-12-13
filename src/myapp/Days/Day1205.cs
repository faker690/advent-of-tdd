using System.Collections.Concurrent;

namespace myapp.Days; public class Day1205 : PuzzleBase
{
    public Day1205()
    {
        Date = new DateOnly(2023, 12, 5);
    }

    public override string ExecutePartOne()
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
        var tmps = infos.Where(x => value >= x.SourceStartNo && value < (x.SourceStartNo + x.Range)).ToList();
        if (tmps.Count == 0) return value;

        return tmps.First().DestStartNo + value - tmps.First().SourceStartNo;
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

    public override string ExecutePartTwo()
    {
        // get middleSet
        var soilInfos = getMiddleInfos(4, 28);
        var fertInfos = getMiddleInfos(31, 39);
        var waterInfos = getMiddleInfos(42, 74);
        var lightInfos = getMiddleInfos(77, 122);
        var tempInfos = getMiddleInfos(125, 159);
        var humInfos = getMiddleInfos(162, 181);
        var locInfos = getMiddleInfos(184, 226);

        long min = -1;
        // get seeds
        var seeds = InputLines[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        for (int i = 0; i < seeds.Count; i += 2)
        {
            var startNo = long.Parse(seeds[i]);
            var range = long.Parse(seeds[i + 1]);

            var tmpList = new ConcurrentBag<long>();
            Parallel.For(0, range, x =>
            {
                var seed = startNo + x;

                var soil = getResult(soilInfos, seed);
                var fertilezer = getResult(fertInfos, soil);
                var water = getResult(waterInfos, fertilezer);
                var light = getResult(lightInfos, water);
                var temperature = getResult(tempInfos, light);
                var humidity = getResult(humInfos, temperature);
                var location = getResult(locInfos, humidity);
                tmpList.Add(location);
            });

            if (i == 0) min = tmpList.Min(x => x);
            else min = Math.Min(min, tmpList.Min());
        }

        return min.ToString();
    }

}

public class MiddleInfo
{
    public long DestStartNo { get; set; }
    public long SourceStartNo { get; set; }
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
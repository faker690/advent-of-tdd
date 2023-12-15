namespace myapp;
public class Day1212 : PuzzleBase
{
    public Day1212()
    {
        Date = new DateOnly(2023, 12, 12);
    }

    public override string PartOne()
    {
        var infos = new List<SpringInfo>();
        foreach (var line in InputLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var info = new SpringInfo { Record = items[0], GroupRecord = items[1] };

            for (int i = 0; i < items[0].Length; i++)
            {
                info.Springs.Add(new Spring
                {
                    Index = i,
                    Sign = items[0][i]
                });
            }
            var itms = items[1].Split(",", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < itms.Length; i++)
            {
                info.GroupInfos.Add(new GroupInfo
                {
                    Index = i,
                    Count = int.Parse(itms[i])
                });
            }

            infos.Add(info);
        }


        foreach (var info in infos)
        {
            var lists = new List<string> { "" };
            for (int i = 0; i < info.Springs.Count; i++)
            {
                var itm = info.Springs[i];
                var count = lists.Count;
                for (int j = 0; j < count; j++)
                {
                    var original = lists[j];
                    if (itm.Status != SpringStatus.Unknown)
                    {
                        lists[j] = $"{original}{itm.Sign}";
                        continue;
                    }

                    var clone = lists[j].Substring(0);
                    lists[j] = $"{original}.";
                    lists.Add($"{clone}#");
                }
            }

            foreach (var itm in lists)
            {
                var items = itm.Split('.', StringSplitOptions.RemoveEmptyEntries);
                if (items.Length != info.GroupInfos.Count) continue;

                var isValid = info.GroupInfos.All(x => items[x.Index].ToString().Length == x.Count);
                if (isValid)
                    info.Arranges++;
            }
        }

        return infos.Sum(x => x.Arranges).ToString();
    }

    public override string PartTwo()
    {
        var infos = new List<SpringInfo>();
        foreach (var line in InputLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var info = new SpringInfo { Record = items[0], GroupRecord = items[1] };

            info.Record = string.Join("?", Enumerable.Repeat(info.Record, 5));
            for (int i = 0; i < info.Record.Length; i++)
            {
                info.Springs.Add(new Spring
                {
                    Index = i,
                    Sign = info.Record[i]
                });
            }

            info.GroupRecord = string.Join(",", Enumerable.Repeat(info.GroupRecord, 5));
            var itms = info.GroupRecord.Split(",", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < itms.Length; i++)
            {
                info.GroupInfos.Add(new GroupInfo
                {
                    Index = i,
                    Count = int.Parse(itms[i])
                });
            }

            infos.Add(info);
        }

        foreach (var info in infos)
        {
            var lists = new List<string> { "" };
            for (int i = 0; i < info.Springs.Count; i++)
            {
                var itm = info.Springs[i];
                var count = lists.Count;
                for (int j = 0; j < count; j++)
                {
                    var original = lists[j];
                    if (itm.Status != SpringStatus.Unknown)
                    {
                        lists[j] = $"{original}{itm.Sign}";
                        continue;
                    }

                    var clone = lists[j].Substring(0);
                    lists[j] = $"{original}.";
                    lists.Add($"{clone}#");
                }
            }

            foreach (var itm in lists)
            {
                var items = itm.Split('.', StringSplitOptions.RemoveEmptyEntries);
                if (items.Length != info.GroupInfos.Count) continue;

                var isValid = info.GroupInfos.All(x => items[x.Index].ToString().Length == x.Count);
                if (isValid)
                    info.Arranges++;
            }
        }

        return infos.Sum(x => x.Arranges).ToString();
    }
}

public class SpringInfo
{
    public SpringInfo()
    {
        GroupInfos = new List<GroupInfo>();
        Springs = new List<Spring>();
    }

    public required string Record { get; set; }
    public required string GroupRecord { get; set; }

    public List<Spring> Springs { get; init; }
    public List<GroupInfo> GroupInfos { get; init; }

    public int Arranges { get; set; }
}

public class GroupInfo
{
    public int Index { get; set; }
    public int Count { get; set; }
}

public class Spring
{
    public int Index { get; set; }
    public char Sign { get; set; }
    public SpringStatus Status => Sign switch
    {
        '.' => SpringStatus.Operational,
        '#' => SpringStatus.Damaged,
        '?' => SpringStatus.Unknown,
        _ => throw new Exception()
    };
}

public enum SpringStatus
{
    Operational = 1,
    Damaged = 2,
    Unknown = 4
}
namespace myapp;
public class Day1213 : PuzzleBase
{
    public Day1213()
    {
        Date = new DateOnly(2023, 12, 13);
    }

    public override string PartOne()
    {
        var infos = new List<PatternInfo>();
        PatternInfo info = new PatternInfo();
        infos.Add(info);

        foreach (var line in InputLines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                info = new PatternInfo();
                infos.Add(info);
                continue;
            }
            info.Records.Add(line);
        }

        foreach (var itm in infos)
        {
            var sum = itm.Summary;
        }
        return infos.Sum(x => x.Summary).ToString();
    }

    public override string PartTwo()
    {

        var infos = new List<PatternInfo>();
        PatternInfo info = new PatternInfo();
        infos.Add(info);

        foreach (var line in InputLines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                info = new PatternInfo();
                infos.Add(info);
                continue;
            }
            info.Records.Add(line);
        }

        long sum = 0;
        foreach (var itm in infos)
        {
            var sum1 = itm.Summary;
            var isSuccess = false;
            for (var j = 0; j < itm.Records.Count; j++)
            {
                for (int i = 0; i < itm.Records[0].Length - 1; i++)
                {
                    var item = itm.GetUpdatedRecords(j, i);
                    var summary = item.Summary;
                    if (summary > 0 && sum1 != summary)
                    {
                        sum += summary;
                        isSuccess = true;
                        break;
                    }
                }

                if (isSuccess) break;
            }
        }
        return sum.ToString();
    }
}

public class PatternInfo
{
    public PatternInfo()
    {
        Records = new List<string>();
    }

    public List<string> Records { get; private set; }

    public long Summary => getSummary();

    public PatternInfo GetUpdatedRecords(int k, int m)
    {
        var info = new PatternInfo();
        for (int i = 0; i < Records.Count; i++)
        {
            if (i != k) info.Records.Add(Records[i]);
            else
            {
                var list = new List<char>();
                for (int j = 0; j < Records[0].Length; j++)
                {
                    if (j != m) list.Add(Records[i][j]);
                    else
                    {
                        if (Records[i][j] == '.') list.Add('#');
                        else list.Add('.');
                    }
                }
                info.Records.Add(string.Join("", list));
            }
        }
        return info;
    }

    public long getSummary()
    {
        // check if horizontal
        double horizontalIndex = 0;
        for (int i = 0; i < Records.Count - 1; i++)
        {
            if (horizontalIndex > 0) break;

            for (int j = Records.Count - 1; j > i; j--)
            {
                if (Records[i] == Records[j] && (j - i) % 2 > 0)
                {
                    if (isValidHorizontalIndex((i + j) / 2.0))
                    {
                        horizontalIndex = (i + j) / 2.0;
                        break;
                    }
                }
            }
        }
        if (horizontalIndex > 0) return (int)Math.Ceiling(horizontalIndex) * 100;

        // check if vertical
        double verticalIndex = 0;
        var length = Records[0].Length;
        for (int i = 0; i < length - 1; i++)
        {
            if (verticalIndex > 0) break;

            for (int j = length - 1; j > i; j--)
            {
                if ((j - i) % 2 > 0 && string.Join("", Records.Select(x => x[i])) == string.Join("", Records.Select(x => x[j])))
                {
                    if (isValidVerticalIndex((i + j) / 2.0))
                    {
                        verticalIndex = (i + j) / 2.0;
                        break;
                    }
                }
            }
        }

        if (verticalIndex > 0) return (int)Math.Ceiling(verticalIndex);

        return 0;
    }

    private bool isValidHorizontalIndex(double index)
    {
        var maxIndex = (int)Math.Floor(index);
        var minIndex = (int)Math.Ceiling(index);

        while (maxIndex >= 0 && minIndex <= Records.Count - 1)
        {
            if (Records[maxIndex] == Records[minIndex])
            {
                maxIndex--;
                minIndex++;
            }
            else
                return false;
        }

        return true;
    }

    private bool isValidVerticalIndex(double index)
    {
        var maxIndex = (int)Math.Floor(index);
        var minIndex = (int)Math.Ceiling(index);

        while (maxIndex >= 0 && minIndex <= Records[0].Length - 1)
        {
            if (string.Join("", Records.Select(x => x[maxIndex])) == string.Join("", Records.Select(x => x[minIndex])))
            {
                maxIndex--;
                minIndex++;
            }
            else
                return false;
        }

        return true;
    }
}



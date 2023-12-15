namespace myapp.Days;
public class Day1206 : PuzzleBase
{
    public Day1206()
    {
        Date = new DateOnly(2023, 12, 6);
    }

    public override string PartOne()
    {
        var times = InputLines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var distances = InputLines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var records = new List<Record>();
        for (int i = 1; i < times.Length; i++)
        {
            var recrod = new Record
            {
                Time = int.Parse(times[i]),
                Distance = int.Parse(distances[i])
            };
            records.Add(recrod);
        }

        var result = 1;
        foreach (var r in records)
        {
            var tmp = 0;
            for (int i = 0; i <= r.Time; i++)
            {
                var maxDistance = i * (r.Time - i);
                if (maxDistance > r.Distance) tmp++;
            }

            result *= tmp;
        }

        return result.ToString();
    }

    public override string PartTwo()
    {
        var time = InputLines[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", "");
        var distance = InputLines[1].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", "");

        var recrod = new Record
        {
            Time = long.Parse(time),
            Distance = long.Parse(distance)
        };

        var result = 0;
        for (long i = 0; i <= recrod.Time; i++)
        {
            var maxDistance = i * (recrod.Time - i);
            if (maxDistance > recrod.Distance) result++;
        }

        return result.ToString();
    }
}

public class Record
{
    public long Time { get; set; }
    public long Distance { get; set; }
}
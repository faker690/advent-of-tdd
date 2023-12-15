namespace myapp.Days;
public class Day1209 : PuzzleBase
{
    public Day1209()
    {
        Date = new DateOnly(2023, 12, 9);
    }

    public override string PartOne()
    {
        long sum = 0;
        foreach (var line in InputLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var list = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            sum += getLineNextHistory(list);
        }
        return sum.ToString();
    }

    private long getLineNextHistory(List<long> list)
    {
        var stack = new Stack<List<long>>();
        stack.Push(list);
        var result = list.Last();
        while (stack.Count > 0)
        {
            var itm = stack.Peek();
            if (itm.All(x => x == 0))
            {
                break;
            }

            var tmp = new List<long>();
            for (int i = 0; i < itm.Count - 1; i++)
            {
                tmp.Add(itm[i + 1] - itm[i]);
            }
            stack.Push(tmp);
            result += tmp.Last();
        }
        return result;
    }

    private long getLinePreHistory(List<long> list)
    {
        var stack = new Stack<List<long>>();
        stack.Push(list);
        while (stack.Count > 0)
        {
            var itm = stack.Peek();
            if (itm.All(x => x == 0))
            {
                break;
            }

            var tmp = new List<long>();
            for (int i = 0; i < itm.Count - 1; i++)
            {
                tmp.Add(itm[i + 1] - itm[i]);
            }
            stack.Push(tmp);
        }

        long lastFirst = 0;
        while (stack.Count > 0)
        {
            var itm = stack.Pop();
            if (itm.All(x => x == 0))
            {
                itm = stack.Pop();
            }
            lastFirst = itm.First() - lastFirst;
        }
        return lastFirst;
    }
    public override string PartTwo()
    {
        long sum = 0;
        foreach (var line in InputLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var list = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            sum += getLinePreHistory(list);
        }
        return sum.ToString();
    }
}
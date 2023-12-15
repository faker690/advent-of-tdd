namespace myapp;
public class Day1211 : PuzzleBase
{
    public Day1211()
    {
        Date = new DateOnly(2023, 12, 11);
    }

    private int _rows;
    private int _columns;

    public override string PartOne()
    {
        _rows = InputLines.Length;
        _columns = InputLines[0].Length;

        // get inputs
        var list = new List<GalaxyInfo>();
        var index = 0;
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                if (InputLines[i][j] == '#')
                {
                    list.Add(new GalaxyInfo
                    {
                        Name = ++index,
                        Row = i,
                        Column = j
                    });
                }
            }
        }

        // twice as big
        // for column twice
        for (int i = _columns - 1; i >= 0; i--)
        {
            var rowCount = list.Where(x => x.Column == i).Count();
            if (rowCount == 0)
            {
                list.Where(x => x.Column > i).ToList().ForEach(x => x.Column++);
            }
        }

        // for row twice
        for (int i = _rows - 1; i >= 0; i--)
        {
            var colCount = list.Where(x => x.Row == i).Count();
            if (colCount == 0)
            {
                list.Where(x => x.Row > i).ToList().ForEach(x => x.Row++);
            }
        }

        // sum the distance
        long sum = 0;
        for (int i = 0; i < index - 1; i++)
        {
            for (int j = i + 1; j < index; j++)
            {
                sum += getDistance(list[i], list[j]);
            }
        }

        return sum.ToString();
    }
    private long getDistance(GalaxyInfo first, GalaxyInfo second)
    {
        return Math.Abs(second.Row - first.Row) + Math.Abs(second.Column - first.Column);
    }

    public override string PartTwo()
    {
        _rows = InputLines.Length;
        _columns = InputLines[0].Length;

        // get inputs
        var list = new List<GalaxyInfo>();
        var index = 0;
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                if (InputLines[i][j] == '#')
                {
                    list.Add(new GalaxyInfo
                    {
                        Name = ++index,
                        Row = i,
                        Column = j
                    });
                }
            }
        }

        // twice as big
        // for column 
        var times = 1000000;
        for (int i = _columns - 1; i >= 0; i--)
        {
            var rowCount = list.Where(x => x.Column == i).Count();
            if (rowCount == 0)
            {
                list.Where(x => x.Column > i).ToList().ForEach(x => x.Column += times - 1);
            }
        }

        // for row twice
        for (int i = _rows - 1; i >= 0; i--)
        {
            var colCount = list.Where(x => x.Row == i).Count();
            if (colCount == 0)
            {
                list.Where(x => x.Row > i).ToList().ForEach(x => x.Row += times - 1);
            }
        }

        // sum the distance
        long sum = 0;
        for (int i = 0; i < index - 1; i++)
        {
            for (int j = i + 1; j < index; j++)
            {
                sum += getDistance(list[i], list[j]);
            }
        }

        return sum.ToString();
    }

}

public class GalaxyInfo
{
    public int Name { get; set; }

    public long Row { get; set; }

    public long Column { get; set; }
}

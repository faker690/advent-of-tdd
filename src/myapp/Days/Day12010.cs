namespace myapp;
public class Day1210 : PuzzleBase
{
    public Day1210()
    {
        Date = new DateOnly(2023, 12, 10);
    }

    private int _rows;
    private int _columns;
    private int _circleCount = 0;
    private Dictionary<int, List<SignInfo>> _circleDic = new Dictionary<int, List<SignInfo>>();

    public override string ExecutePartOne()
    {
        // get data inputs
        var data = new List<SignInfo>();
        _rows = InputLines.Length;
        _columns = InputLines[0].Length;

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                var ch = InputLines[i][j];
                var tmp = getDirectInfo(ch);
                var item = new SignInfo
                {
                    Sign = ch,
                    Row = i,
                    Column = j,
                    Start = tmp.Item1,
                    End = tmp.Item2
                };
                data.Add(item);
            }
        }

        // calculate the distance
        var startSignInfo = data.First(x => x.Sign == 'S');
        startSignInfo.Distance = 0;

        breadFirstSearch(data, startSignInfo);

        return data.Max(x => x.Distance).ToString();
    }

    public override string ExecutePartTwo()
    {
        // get data inputs
        var data = new List<SignInfo>();
        _rows = InputLines.Length;
        _columns = InputLines[0].Length;

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                var ch = InputLines[i][j];
                var tmp = getDirectInfo(ch);
                var item = new SignInfo
                {
                    Sign = ch,
                    Row = i,
                    Column = j,
                    Start = tmp.Item1,
                    End = tmp.Item2
                };
                data.Add(item);
            }
        }

        // calculate the distance
        var startSignInfo = data.First(x => x.Sign == 'S');
        startSignInfo.Distance = 0;

        breadFirstSearch(data, startSignInfo);

        var circleInfos = data.Where(x => x.IsCircleStart()).ToList();
        foreach (var info in circleInfos)
        {
            var list = new List<SignInfo>();
            findCircleInfos(info, list);
        }

        return data.Max(x => x.Distance).ToString();
    }

    private void findCircleInfos(SignInfo info, List<SignInfo> results)
    {
        if (info.Neighbors.Count < 2)
        {
            results.Clear();
            return;
        }

        foreach (var item in info.Neighbors)
        {
            if (results.Any(x => x.Row == item.Row && x.Column == item.Column))
            {
                _circleDic[++_circleCount] = results;
                results = new List<SignInfo>();
                break;
            }

            results.Add(item);
            findCircleInfos(item, results);
        }
    }

    private void breadFirstSearch(List<SignInfo> datas, SignInfo info)
    {
        var queue = new Queue<SignInfo>();
        queue.Enqueue(info);

        while (queue.Count > 0)
        {
            var tmp = queue.Dequeue();

            var children = findNeighbors(datas, tmp);

            foreach (var child in children)
            {
                queue.Enqueue(child);
            }
        }
    }

    private List<SignInfo> findNeighbors(List<SignInfo> datas, SignInfo signInfo)
    {
        var findNeighbors = new List<SignInfo>();
        var currentRow = signInfo.Row;
        var currentCol = signInfo.Column;
        // top / bottom, left / right
        var topRow = signInfo.Row - 1;
        var btmRow = signInfo.Row + 1;
        var leftCol = signInfo.Column - 1;
        var rightCol = signInfo.Column + 1;

        var topSignInfo = datas.FirstOrDefault(x => x.Row == topRow && x.Column == signInfo.Column);
        if (topSignInfo != null)
        {
            if (topSignInfo.Start == DirectInfo.South || topSignInfo.End == DirectInfo.South)
            {
                signInfo.Neighbors.Add(topSignInfo);
                if (topSignInfo.Distance == -1)
                {
                    topSignInfo.Distance = signInfo.Distance + 1;
                    findNeighbors.Add(topSignInfo);
                }
            }
        }

        var btmSignInfo = datas.FirstOrDefault(x => x.Row == btmRow && x.Column == signInfo.Column);
        if (btmSignInfo != null)
        {
            if (btmSignInfo.Start == DirectInfo.North || btmSignInfo.End == DirectInfo.North)
            {
                signInfo.Neighbors.Add(btmSignInfo);
                if (btmSignInfo.Distance == -1)
                {
                    btmSignInfo.Distance = signInfo.Distance + 1;
                    findNeighbors.Add(btmSignInfo);
                }
            }
        }

        var leftSignInfo = datas.FirstOrDefault(x => x.Row == signInfo.Row && x.Column == leftCol);
        if (leftSignInfo != null)
        {
            if (leftSignInfo.Start == DirectInfo.East || leftSignInfo.End == DirectInfo.East)
            {
                signInfo.Neighbors.Add(leftSignInfo);
                if (leftSignInfo.Distance == -1)
                {
                    leftSignInfo.Distance = signInfo.Distance + 1;
                    findNeighbors.Add(leftSignInfo);
                }
            }
        }

        var rightSignInfo = datas.FirstOrDefault(x => x.Row == signInfo.Row && x.Column == rightCol);
        if (rightSignInfo != null)
        {
            if (rightSignInfo.Start == DirectInfo.West || rightSignInfo.End == DirectInfo.West)
            {
                signInfo.Neighbors.Add(rightSignInfo);
                if (rightSignInfo.Distance == -1)
                {
                    rightSignInfo.Distance = signInfo.Distance + 1;
                    findNeighbors.Add(rightSignInfo);
                }
            }
        }

        return findNeighbors;
    }

    private (DirectInfo, DirectInfo) getDirectInfo(char ch)
    {
        return ch switch
        {
            '|' => (DirectInfo.North, DirectInfo.South),
            '-' => (DirectInfo.East, DirectInfo.West),
            'L' => (DirectInfo.North, DirectInfo.East),
            'J' => (DirectInfo.North, DirectInfo.West),
            '7' => (DirectInfo.South, DirectInfo.West),
            'F' => (DirectInfo.South, DirectInfo.East),
            '.' => (DirectInfo.None, DirectInfo.None),
            'S' => (DirectInfo.None, DirectInfo.None),
            _ => throw new Exception()
        };
    }
}

public class SignInfo
{
    public SignInfo() => Neighbors = new List<SignInfo>();

    public char Sign { get; set; }
    public DirectInfo Start { get; set; }
    public DirectInfo End { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
    public int Distance { get; set; } = -1;

    public List<SignInfo> Neighbors { get; init; }

    public bool IsCircleStart()
    {
        if (Neighbors.Count <= 1) return false;

        var diffDistance = Neighbors.Max(x => x.Distance) - Neighbors.Min(x => x.Distance);
        return diffDistance > 2;
    }
}

public enum DirectInfo
{
    None,
    North,
    South,
    East,
    West
}
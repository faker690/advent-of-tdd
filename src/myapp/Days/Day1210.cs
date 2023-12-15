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

    public override string PartOne()
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

    public override string PartTwo()
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

        var startSignInfo = data.First(x => x.Sign == 'S');
        startSignInfo.Distance = 0;

        //
        deepthFirstSearch(data, startSignInfo);

        var maxCircle = _circleDic.Max(x => x.Value.Count);
        var circleDic = _circleDic.Where(x => x.Value.Count == maxCircle).ToList();


        var circleInfos = circleDic.First().Value;
        var minRow = circleInfos.Min(x => x.Row);
        var maxRow = circleInfos.Max(x => x.Row);
        var minCol = circleInfos.Min(x => x.Column);
        var maxCol = circleInfos.Max(x => x.Column);
        var items = new List<SignInfo>();

        for (int r = minRow + 1; r < maxRow; r++)
        {
            for (int c = minCol + 1; c < maxCol; c++)
            {
                var itm = data.First(x => x.Row == r && x.Column == c);
                if (itm.Sign != '.') continue;


                var check = circleInfos.Any(x => x.Row > r && x.Column == c);
                check = check && circleInfos.Any(x => x.Row < r && x.Column == c);
                check = check && circleInfos.Any(x => x.Row == r && x.Column > c);
                check = check && circleInfos.Any(x => x.Row == r && x.Column < c);

                if (check)
                {
                    var rowCount = circleInfos.Count(x => x.Row == r);
                    var colCount = circleInfos.Count(x => x.Column == c);
                    if ((rowCount + colCount) % 2 == 1) items.Add(itm);
                }
            }
        }


        return items.Count.ToString();
    }

    private void deepthFirstSearch(List<SignInfo> datas, SignInfo info)
    {
        var stack = new Stack<SignInfo>();
        info.IsVisited = true;
        stack.Push(info);

        while (stack.Count > 0)
        {
            var itm = stack.Peek();

            if (itm.Neighbors.Count == 0)
                findNeighbors(datas, itm);

            if (itm.Children.Count == 0)
            {
                stack.Pop();
                continue;
            }

            foreach (var child in itm.Children)
            {
                if (!child.IsVisited)
                {
                    child.IsVisited = true;
                    stack.Push(child);
                    break;
                }

                _circleDic[++_circleCount] = stack.Reverse().ToList();
                stack.Pop();
            }
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
                topSignInfo.Previous.Add(signInfo);
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
                btmSignInfo.Previous.Add(signInfo);
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
                leftSignInfo.Previous.Add(signInfo);
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
                rightSignInfo.Previous.Add(signInfo);
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
    public SignInfo()
    {
        Neighbors = new List<SignInfo>();
        Previous = new List<SignInfo>();
    }
    public char Sign { get; set; }
    public DirectInfo Start { get; set; }
    public DirectInfo End { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
    public int Distance { get; set; } = -1;

    public List<SignInfo> Neighbors { get; init; }
    public List<SignInfo> Previous { get; init; }

    public List<SignInfo> Children => Neighbors.Where(x => !Previous.Any(y => x.Row == y.Row && x.Column == y.Column)).ToList();

    public bool IsVisited { get; set; }
}

public enum DirectInfo
{
    None,
    North,
    South,
    East,
    West
}
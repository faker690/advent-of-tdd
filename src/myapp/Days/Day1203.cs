namespace myapp.Days;
public class Day1203 : PuzzleBase
{
    public Day1203()
    {
        Date = new DateOnly(2023, 12, 3);
        _mockInputs = getMockInputs();
    }

    private List<string> _mockInputs;

    private List<EngineInfo> getEngineInfos()
    {
        // find the number related block
        var engineInfos = new List<EngineInfo>();

        for (int i = 1; i < _mockInputs.Count - 1; i++)
        {
            var line = _mockInputs[i];
            var offset = 0;
            var sub = line.Substring(offset);

            while (Regex.IsMatch(sub, "\\d+"))
            {
                var match = Regex.Match(sub, "\\d+");
                var data = match.ToString();
                var relativeIndex = sub.IndexOf(data);

                var info = new EngineInfo
                {
                    Row = i,
                    StartIndex = relativeIndex - 1 + offset,
                    Number = int.Parse(data)
                };
                engineInfos.Add(info);

                offset += relativeIndex + data.Length;
                sub = line.Substring(offset);
            }
        }
        return engineInfos;
    }

    private List<string> getMockInputs()
    {
        // add top/bottom line for all (.).
        // add left/right column for all (.).
        var lineLength = InputLines[0].Length;
        var mockInputs = new List<string>();
        mockInputs.Add(new string('.', lineLength + 2));
        foreach (var line in InputLines)
        {
            mockInputs.Add($".{line}.");
        }
        mockInputs.Add(new string('.', lineLength + 2));

        return mockInputs;
    }

    private bool isSymbol(char ch)
    {
        if (ch == '.') return false;
        if (int.TryParse(ch.ToString(), out int _)) return false;

        return true;
    }

    private bool hasSymbol(string data)
    {
        if (string.IsNullOrWhiteSpace(data)) return false;
        return data.ToList().Any(isSymbol);
    }

    public override string PartOne()
    {
        var engineInfos = getEngineInfos();

        // check the number is adjacent to a symbol
        var sum = 0;
        foreach (var info in engineInfos)
        {
            var length = info.Number.ToString().Length + 2;
            var topString = _mockInputs[info.Row - 1].Substring(info.StartIndex, length);
            var middleString = _mockInputs[info.Row].Substring(info.StartIndex, length);
            var bottomString = _mockInputs[info.Row + 1].Substring(info.StartIndex, length);
            if (hasSymbol(topString) || hasSymbol(middleString) || hasSymbol(bottomString))
            {
                sum += info.Number;
                continue;
            }
        }

        return sum.ToString();
    }

    public override string PartTwo()
    {
        var symbolInfos = getSymbolInfos();
        var engineInfos = getEngineInfos();

        // check the number is adjacent to a symbol
        var sum = 0;
        foreach (var symbol in symbolInfos)
        {
            var minRow = symbol.Row - 1;
            var maxRow = symbol.Row + 1;
            var leftIndex = symbol.StartIndex - 1;
            var rightIndex = symbol.StartIndex + 1;
            var relatedIndexes = new List<int> { leftIndex, symbol.StartIndex, rightIndex };

            var items = engineInfos.Where(x =>
                x.Row >= minRow && x.Row <= maxRow &&
                x.GetRelatedIndexes().Any(y => relatedIndexes.Any(z => z == y))).ToList();

            foreach (var item in items)
            {
                foreach (var item1 in items)
                {
                    if (item == item1) continue;
                    sum += item.Number * item1.Number;
                }
            }
        }

        return (sum / 2).ToString();
    }

    private List<SymbolInfo> getSymbolInfos()
    {
        // find the symbol related block
        var symbolInfos = new List<SymbolInfo>();

        for (int i = 1; i < _mockInputs.Count - 1; i++)
        {
            var line = _mockInputs[i];
            var offset = 0;
            var sub = line.Substring(offset);

            while (Regex.IsMatch(sub, "\\*"))
            {
                var match = Regex.Match(sub, "\\*");
                var relativeIndex = sub.IndexOf(match.ToString());

                var info = new SymbolInfo
                {
                    Row = i,
                    StartIndex = relativeIndex - 1 + offset,
                };

                symbolInfos.Add(info);

                offset += relativeIndex + match.ToString().Length;
                sub = line.Substring(offset);
            }
        }
        return symbolInfos;
    }
}

public class EngineInfo
{
    public int Row { get; set; }
    public int StartIndex { get; set; }
    public int Number { get; set; }

    public List<int> GetRelatedIndexes()
    {
        var list = new List<int>();
        for (int i = 0; i < Number.ToString().Length; i++)
        {
            list.Add(StartIndex + i);
        }
        return list;
    }
}

public class SymbolInfo
{
    public int Row { get; set; }
    public int StartIndex { get; set; }
}

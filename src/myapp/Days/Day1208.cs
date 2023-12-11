namespace myapp.Days;
public class Day1208 : PuzzleBase
{
    public Day1208()
    {
        Date = new DateOnly(2023, 12, 8);
    }

    public override string ExecutePartOne()
    {
        var commands = InputLines[0];
        var nodeInfos = new List<NodeInfo>();
        for (int i = 0; i < InputLines.Length; i++)
        {
            var items = InputLines[i].Split("=");
            if (items.Length != 2) continue;

            var leftRight = items[1].Replace("(", "").Replace(")", "").Trim();
            var node = new NodeInfo
            {
                Name = items[0].Trim(),
                Left = leftRight.Split(",")[0].Trim(),
                Right = leftRight.Split(",")[1].Trim(),
            };
            nodeInfos.Add(node);
        }

        var isSuccess = false;
        var currentNode = nodeInfos.First(x => x.Name == "AAA");
        while (!isSuccess)
        {
            foreach (var command in commands)
            {
                currentNode.Count++;

                if (command == 'R')
                {
                    if (currentNode.Right == "ZZZ")
                    {
                        isSuccess = true;
                        break;
                    }

                    currentNode = nodeInfos.First(x => x.Name == currentNode.Right);
                }
                else if (command == 'L')
                {
                    if (currentNode.Left == "ZZZ")
                    {
                        isSuccess = true;
                        break;
                    }
                    currentNode = nodeInfos.First(x => x.Name == currentNode.Left);
                }
                else throw new Exception();

            }
        }

        return nodeInfos.Sum(x => x.Count).ToString();
    }

    public override string ExecutePartTwo()
    {
        var commands = InputLines[0];
        var nodeInfos = new List<NodeInfo>();
        for (int i = 0; i < InputLines.Length; i++)
        {
            var items = InputLines[i].Split("=");
            if (items.Length != 2) continue;

            var leftRight = items[1].Replace("(", "").Replace(")", "").Trim();
            var node = new NodeInfo
            {
                Name = items[0].Trim(),
                Left = leftRight.Split(",")[0].Trim(),
                Right = leftRight.Split(",")[1].Trim(),
            };
            nodeInfos.Add(node);
        }

        var isSuccess = false;
        var totalSteps = 0;
        var currentNodes = nodeInfos.Where(x => x.Name[2] == 'A').ToList();
        while (!isSuccess)
        {
            foreach (var command in commands)
            {
                totalSteps++;

                if (command == 'R')
                {
                    if (currentNodes.All(x => x.Right[2] == 'Z'))
                    {
                        isSuccess = true;
                        break;
                    }

                    currentNodes = nodeInfos.Where(x => currentNodes.Any(y => y.Right == x.Name)).ToList();
                }
                else if (command == 'L')
                {
                    if (currentNodes.All(x => x.Left[2] == 'Z'))
                    {
                        isSuccess = true;
                        break;
                    }
                    currentNodes = nodeInfos.Where(x => currentNodes.Any(y => y.Left == x.Name)).ToList();
                }
                else throw new Exception();
            }
        }

        return totalSteps.ToString();
    }
}

public class NodeInfo
{
    public required string Name { get; set; }
    public required string Left { get; set; }
    public required string Right { get; set; }

    public int Count { get; set; }
}
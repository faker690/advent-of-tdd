namespace myapp.Days;
public class Day1208 : PuzzleBase
{
    public Day1208()
    {
        Date = new DateOnly(2023, 12, 8);
    }

    public override string PartOne()
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

    // not complete
    public override string PartTwo()
    {
        var commands = InputLines[0];
        var nodeDic = new Dictionary<string, NodeInfo>();
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
            nodeDic[node.Name] = node;
        }

        var isSuccess = false;
        long totalSteps = 0;
        var nodeNames = nodeDic.Keys.Where(x => x[2] == 'A').ToList();
        var currentNodes = nodeNames.Select(x => nodeDic[x]).ToList();
        while (!isSuccess)
        {
            foreach (var command in commands)
            {
                totalSteps++;

                nodeNames = command switch
                {
                    'R' => currentNodes.Select(x => x.Right).ToList(),
                    'L' => currentNodes.Select(x => x.Left).ToList(),
                    _ => throw new Exception()
                };

                if (nodeNames.Any(x => x[2] != 'Z'))
                {
                    currentNodes = nodeNames.Select(x => nodeDic[x]).ToList();
                    continue;
                }
                isSuccess = true;
                break;
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
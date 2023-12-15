using System.Data;

namespace myapp;
public class Day1214 : PuzzleBase
{
    public Day1214()
    {
        Date = new DateOnly(2023, 12, 14);
    }

    public override string PartOne()
    {
        var rowCount = InputLines.Length;
        var colCount = InputLines[0].Length;

        var listRocks = new List<Rock>();
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                listRocks.Add(new Rock
                {
                    Row = i,
                    Col = j,
                    Sign = InputLines[i][j]
                });
            }
        }

        listRocks.Where(x => x.Sign != '.').ToList().ForEach(x => x.UpdatedRow = x.Row);

        // col
        for (int i = 0; i < colCount; i++)
        {
            // row
            for (int j = 1; j < rowCount; j++)
            {
                var rock = listRocks.First(x => x.Row == j && x.Col == i);
                if (rock.Sign == '#' || rock.Sign == '.') continue;

                var lastRow = listRocks.Where(x => x.Col == i && x.Row < j).Max(x => x.UpdatedRow);
                rock.UpdatedRow = lastRow + 1;
            }
        }

        return listRocks.Where(x => x.Sign == 'O').Sum(x => rowCount - x.UpdatedRow).ToString();
    }


    // not complete
    public override string PartTwo()
    {
        var rowCount = InputLines.Length;
        var colCount = InputLines[0].Length;

        var listRocks = new List<Rock>();
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                listRocks.Add(new Rock
                {
                    Row = i,
                    Col = j,
                    Sign = InputLines[i][j]
                });
            }
        }

        listRocks.Where(x => x.Sign != '.').ToList().ForEach(x =>
        {
            x.UpdatedCol = x.Col;
            x.UpdatedRow = x.Row;
        });

        for (long k = 0; k < 1000000000; k++)
        {
            System.Console.WriteLine($"index: {k}");
            foreach (var rock in listRocks)
            {
                // north
                var upItems = listRocks.Where(x => x.UpdatedCol == rock.UpdatedCol && x.UpdatedRow <= rock.UpdatedRow).ToList();
                var upStart = 0;
                var upCube = upItems.LastOrDefault(x => x.Sign == '#');
                if (upCube != null) upStart = upCube.UpdatedRow;
                var upRocks = upItems.Count(x => x.UpdatedRow > upStart && x.UpdatedRow <= rock.UpdatedRow);
                rock.UpdatedRow = upStart + upRocks;
            }

            foreach (var rock in listRocks)
            {
                // west
                var leftItems = listRocks.Where(x => x.UpdatedRow == rock.UpdatedRow && x.UpdatedCol <= rock.UpdatedCol).ToList();
                var leftStart = 0;
                var leftCube = leftItems.LastOrDefault(x => x.Sign == '#');
                if (leftCube != null) leftStart = leftCube.UpdatedCol;
                var leftRocks = leftItems.Count(x => x.UpdatedCol > leftStart && x.UpdatedCol <= rock.UpdatedCol);
                rock.UpdatedCol = leftStart + leftRocks;
            }

            foreach (var rock in listRocks)
            {
                // sourth
                var btmItems = listRocks.Where(x => x.UpdatedCol == rock.UpdatedCol && x.UpdatedRow >= rock.UpdatedRow).ToList();
                var btmEnd = rowCount;
                var btmCube = btmItems.FirstOrDefault(x => x.Sign == '#');
                if (btmCube != null) btmEnd = btmCube.UpdatedRow;
                var btmRocks = btmItems.Count(x => x.UpdatedRow < btmEnd && x.UpdatedRow >= rock.UpdatedRow);
                rock.UpdatedRow = btmEnd - btmRocks;
            }

            foreach (var rock in listRocks)
            {
                // east
                var rightItems = listRocks.Where(x => x.UpdatedRow == rock.UpdatedRow && x.UpdatedCol >= rock.UpdatedCol).ToList();
                var rightEnd = colCount;
                var rightCube = rightItems.FirstOrDefault(x => x.Sign == '#');
                if (rightCube != null) rightEnd = rightCube.UpdatedCol;
                var rightRocks = rightItems.Count(x => x.UpdatedCol < rightEnd && x.UpdatedCol >= rock.UpdatedCol);
                rock.UpdatedCol = rightEnd - rightRocks;
            }
        }

        return listRocks.Where(x => x.Sign == 'O').Sum(x => rowCount - x.UpdatedRow).ToString();
    }
}

public class Rock
{
    public int Row { get; set; }
    public int Col { get; set; }
    public char Sign { get; set; }
    public int UpdatedRow { get; set; } = -1;
    public int UpdatedCol { get; set; } = -1;
}


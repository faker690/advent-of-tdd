namespace myapp.Days;
public class Day1202 : PuzzleBase
{
    public Day1202()
    {
        Date = new DateOnly(2023, 12, 2);
    }

    private List<GameInfo> execute()
    {
        var results = new List<GameInfo>();
        foreach (var line in InputLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var items = line.ToLower().Split(":", StringSplitOptions.RemoveEmptyEntries);
            if (items.Length != 2) continue;

            int.TryParse(items[0].Replace("game ", ""), out int row);
            var gameInfo = new GameInfo { Index = row };
            foreach (var tmp in items[1].Split(";", StringSplitOptions.TrimEntries))
            {
                var game = new Game();
                var cubePhases = tmp.Split(",", StringSplitOptions.TrimEntries);
                foreach (var cube in cubePhases)
                {
                    var phase = cube.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    switch (phase[1])
                    {
                        case "red":
                            game.RedCube = int.Parse(phase[0]);
                            break;
                        case "blue":
                            game.BlueCube = int.Parse(phase[0]);
                            break;
                        case "green":
                            game.GreenCube = int.Parse(phase[0]);
                            break;
                    }
                }
                gameInfo.Games.Add(game);
            }
            results.Add(gameInfo);
        }

        return results;
    }

    public override string PartOne()
    {
        return execute().Where(x => x.Games.All(y => y.IsValid)).Select(x => x.Index).Distinct().Sum(x => x).ToString();
    }

    public override string PartTwo()
    {
        var total = 0;
        execute().ForEach(x =>
        {
            var maxRed = x.Games.Max(y => y.RedCube);
            var maxGreen = x.Games.Max(y => y.GreenCube);
            var maxBlue = x.Games.Max(y => y.BlueCube);

            var gameSum = Math.Max(1, maxRed) * Math.Max(1, maxGreen) * Math.Max(1, maxBlue);
            total += gameSum;
        });

        return total.ToString();
    }
}

public class GameInfo
{
    public GameInfo()
    {
        Games = new List<Game>();
    }

    public int Index { get; set; }
    public List<Game> Games { get; init; }
}

public class Game
{
    public int RedCube { get; set; }
    public int BlueCube { get; set; }
    public int GreenCube { get; set; }

    public bool IsValid => RedCube <= 12 && BlueCube <= 14 && GreenCube <= 13;

}
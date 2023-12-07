namespace myapp;

public abstract class PuzzleBase
{
    public DateOnly Date { get; set; }

    public string[] InputLines => Helper.GetInputLines(Date.Day);

    public abstract string GetResult(int order);

}


namespace myapp.Model;
using myapp.Helper;

public abstract class PuzzleBase
{

    public DateOnly Date { get; set; }

    public string[] InputLines => Helper.GetInputLines(Date.Day);

    public abstract string PartOne();

    public abstract string PartTwo();

}

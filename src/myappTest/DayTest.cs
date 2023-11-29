namespace myappTest;

public class DayTest
{
    [Fact]
    public void Test1()
    {
        var day = new Day();
        Assert.Equal(2, day.Add(1, 1));
    }

    [Fact]
    public void TestName()
    {
         var day = new Day();
        Assert.False(3 == day.Add(1, 1));
    }
}
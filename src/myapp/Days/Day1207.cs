namespace myapp.Days;
public class Day1207 : PuzzleBase
{
    public Day1207()
    {
        Date = new DateOnly(2023, 12, 7);
    }

    public override string PartOne()
    {
        var cardHands = new List<CardHand>();
        foreach (var line in InputLines)
        {
            var items = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var cardHand = new CardHand
            {
                Hand = items[0],
                Bid = int.Parse(items[1])
            };
            cardHands.Add(cardHand);
        }

        var dic = cardHands.GroupBy(x => x.SameLblCount()).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.ToList());
        var offset = 0;
        foreach (var d in dic)
        {
            var len = d.Value.Count;
            for (int i = 0; i < len - 1; i++)
            {
                var minIndex = i;
                for (int j = i + 1; j < len; j++)
                {
                    if (d.Value[minIndex].IsLarger(d.Value[j]))
                    {
                        minIndex = j;
                    }
                }
                var tmp = d.Value[i];
                d.Value[i] = d.Value[minIndex];
                d.Value[minIndex] = tmp;
            }

            var rank = 1;
            d.Value.ForEach(x => x.Rank = offset + rank++);
            offset += len;
        }

        long sum = 0;
        foreach (var d in dic)
        {
            foreach (var item in d.Value)
                sum += item.Rank * item.Bid;
        }
        return sum.ToString();
    }

    public override string PartTwo()
    {
        var cardHands = new List<CardHandTwo>();
        foreach (var line in InputLines)
        {
            var items = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var cardHand = new CardHandTwo
            {
                Hand = items[0],
                Bid = int.Parse(items[1])
            };
            cardHands.Add(cardHand);
        }

        var dic = cardHands.GroupBy(x => x.SameLblCount()).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.ToList());
        var offset = 0;
        foreach (var d in dic)
        {
            var len = d.Value.Count;
            for (int i = 0; i < len - 1; i++)
            {
                var minIndex = i;
                for (int j = i + 1; j < len; j++)
                {
                    if (d.Value[minIndex].IsLarger(d.Value[j]))
                    {
                        minIndex = j;
                    }
                }
                var tmp = d.Value[i];
                d.Value[i] = d.Value[minIndex];
                d.Value[minIndex] = tmp;
            }

            var rank = 1;
            d.Value.ForEach(x => x.Rank = offset + rank++);
            offset += len;
        }

        long sum = 0;
        foreach (var d in dic)
        {
            foreach (var item in d.Value)
                sum += item.Rank * item.Bid;
        }
        return sum.ToString();
    }
}

public class CardHand
{
    public CardHand()
    {
        // init part one
        var array = new char[] { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
        var max = 14;
        for (int i = 0; i < array.Length; i++)
        {
            BaseOrder[array[i]] = max - i;
        }
    }

    private Dictionary<char, int> BaseOrder = new Dictionary<char, int>();

    public required string Hand { get; set; }
    public int Bid { get; set; }
    public int Rank { get; set; }

    /// <summary>
    /// 10 - high card
    /// 21 - one pair
    /// 22 - two pair
    /// 31 - Three of a kind
    /// 32 - Full house
    /// 40 - four of a kind
    /// 50 - five of a kind
    /// </summary>
    /// <returns></returns>
    public int SameLblCount()
    {
        var max = 0;
        for (int i = 0; i < Hand.Length; i++)
        {
            var count = Hand.ToList().Count(x => x == Hand[i]);
            max = Math.Max(max, count);
        }

        if (max == 2)
        {
            var times = 0;
            for (int i = 0; i < Hand.Length; i++)
            {
                var count = Hand.ToList().Count(x => x == Hand[i]);
                if (count == 2) times++;
            }

            return 20 + times / 2;
        }

        if (max == 3)
        {
            var times = 0;
            for (int i = 0; i < Hand.Length; i++)
            {
                var count = Hand.ToList().Count(x => x == Hand[i]);
                if (count == 3)
                {
                    var chars = Hand.Where(x => x != Hand[i]).Distinct().Count();
                    times = chars == 1 ? 2 : 1;
                    break;
                };
            }
            return 30 + times;
        }

        return 10 * max;
    }

    public bool IsLarger(CardHand cardHand)
    {
        if (SameLblCount() > cardHand.SameLblCount()) return true;
        if (SameLblCount() < cardHand.SameLblCount()) return false;

        for (int i = 0; i < Hand.Length; i++)
        {
            if (BaseOrder[Hand[i]] > BaseOrder[cardHand.Hand[i]]) return true;
            if (BaseOrder[Hand[i]] < BaseOrder[cardHand.Hand[i]]) return false;
        }

        throw new Exception("Same String Value");
    }
}

public class CardHandTwo
{

    public CardHandTwo()
    {
        // init part two
        var max = 14;
        var array = new char[] { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };
        for (int i = 0; i < array.Length; i++)
        {
            BaseOrder[array[i]] = max - i;
        }
    }

    private Dictionary<char, int> BaseOrder = new Dictionary<char, int>();
    public required string Hand { get; set; }
    public int JCount => Hand.ToList().Count(x => x == 'J');
    public int Bid { get; set; }
    public int Rank { get; set; }

    /// <summary>
    /// 10 - high card
    /// 21 - one pair
    /// 22 - two pair
    /// 31 - Three of a kind
    /// 32 - Full house
    /// 40 - four of a kind
    /// 50 - five of a kind
    /// </summary>
    /// <returns></returns>
    public int SameLblCount()
    {
        var max = 0;
        for (int i = 0; i < Hand.Length; i++)
        {
            if (Hand[i] == 'J') continue;

            var count = Hand.ToList().Count(x => x == Hand[i]);
            max = Math.Max(max, count);
        }

        if (max == 0) return 50;

        if (max == 1)
        {
            var chars = Hand.Where(x => x != 'J').Distinct().Count();
            if (chars == 5) return 10;
            if (chars == 4) return 21;
            if (chars == 3) return 31;
            if (chars == 2) return 40;
            if (chars == 1) return 50;
        }

        if (max == 2)
        {
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] == 'J') continue;

                var count = Hand.ToList().Count(x => x == Hand[i]);
                if (count == 2)
                {
                    var chars = Hand.Where(x => x != Hand[i] && x != 'J').Distinct().Count();
                    if (chars == 3) return 21;
                    if (chars == 2)
                    {
                        if (JCount == 1) return 31;
                        else if (JCount == 0) return 22;

                        throw new Exception();
                    }
                    if (chars == 1)
                    {
                        if (JCount == 1) return 32;
                        if (JCount == 2) return 40;

                        throw new Exception();
                    }
                    if (chars == 0)
                    {
                        if (JCount == 3) return 50;

                        throw new Exception();
                    }
                }
            }
        }

        if (max == 3)
        {
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] == 'J') continue;

                var count = Hand.ToList().Count(x => x == Hand[i]);
                if (count == 3)
                {
                    var chars = Hand.Where(x => x != Hand[i] && x != 'J').Distinct().Count();
                    if (chars == 2) return 31;
                    if (chars == 1)
                    {
                        if (JCount == 1) return 40;
                        if (JCount == 0) return 32;

                        throw new Exception();
                    }

                    if (chars == 0) return 50;
                };
            }
        }

        if (max == 4)
        {
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] == 'J') continue;

                var count = Hand.ToList().Count(x => x == Hand[i]);
                if (count == 4)
                {
                    var chars = Hand.Where(x => x != Hand[i] && x != 'J').Distinct().Count();
                    if (chars == 1) return 40;
                    if (chars == 0) return 50;
                };
            }
        }

        if (max == 5) return 50;

        throw new Exception();
    }

    public bool IsLarger(CardHandTwo cardHand)
    {
        if (SameLblCount() > cardHand.SameLblCount()) return true;
        if (SameLblCount() < cardHand.SameLblCount()) return false;

        for (int i = 0; i < Hand.Length; i++)
        {
            if (BaseOrder[Hand[i]] > BaseOrder[cardHand.Hand[i]]) return true;
            if (BaseOrder[Hand[i]] < BaseOrder[cardHand.Hand[i]]) return false;
        }

        throw new Exception("Same String Value");
    }
}
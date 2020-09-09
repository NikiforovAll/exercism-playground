using System;
using System.Collections.Generic;
using System.Linq;

public class HighScores
{
    private readonly List<int> _list;
    public HighScores(List<int> list) => _list = list;

    public List<int> Scores() => _list.ToList();

    public int Latest() => _list.Last();

    public int PersonalBest() => _list.Max();

    public List<int> PersonalTopThree() =>
        _list.OrderByDescending(i => i).Take(3).ToList();
}

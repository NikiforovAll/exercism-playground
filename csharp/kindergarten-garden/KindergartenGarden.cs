using System;
using System.Collections.Generic;
using System.Linq;

public enum Plant
{
    Violets,
    Radishes,
    Clover,
    Grass
}

public class KindergartenGarden
{
    private readonly string[][] _rows;

    public KindergartenGarden(string diagram) =>
        _rows = diagram.Split('\n').Select(s => Slide(s).ToArray()).ToArray();

    public IEnumerable<Plant> Plants(string student)
    {
        var @class = new List<string> {
            "Alice", "Bob", "Charlie", "David",
            "Eve", "Fred", "Ginny", "Harriet",
            "Ileana", "Joseph", "Kincaid", "Larry"
        };
        var ind = @class.IndexOf(student);
        return _rows.SelectMany(r => r[ind].Select(code => code switch
        {
            'V' => Plant.Violets,
            'R' => Plant.Radishes,
            'C' => Plant.Clover,
            'G' => Plant.Grass,
        })).ToArray();
    }

    private IEnumerable<string> Slide(string str)
    {
        var i = 1;
        while (i < str.Length)
        {
            yield return str[(i - 1)..(i + 1)];
            i += 2;
        }
    }
}

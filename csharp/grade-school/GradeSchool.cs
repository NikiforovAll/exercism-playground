using System;
using System.Collections.Generic;
using System.Linq;

public class GradeSchool
{
    private readonly SortedList<(string n, int g), (string n, int g)> _roaster =
        new SortedList<(string n, int g), (string n, int g)>(
        Comparer<(string n, int g)>.Create(
            (x, y) => x.g != y.g
                ? Comparer<int>.Default.Compare(x.g, y.g)
                : Comparer<string>.Default.Compare(x.n, y.n)));
    public void Add(string student, int grade) => _roaster.Add((student, grade), (student, grade));

    public IEnumerable<string> Roster() => _roaster.Values.Select(entry => entry.n);

    public IEnumerable<string> Grade(int grade) =>
        _roaster.Keys.Where(entry => entry.g == grade).Select(entry => entry.n);
}

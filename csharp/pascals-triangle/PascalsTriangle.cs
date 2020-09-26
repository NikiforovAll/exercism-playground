using System;
using System.Collections.Generic;
using System.Linq;

public static class PascalsTriangle
{
    public static IEnumerable<IEnumerable<int>> Calculate(int rows)
    {
        ICollection<IEnumerable<int>> container = new List<IEnumerable<int>>();
        for (int line = 1; line <= rows; line++)
        {
            container.Add(BuildLine(line).ToList());
        }
        static IEnumerable<int> BuildLine(int line)
        {
            var C = 1;
            for (int i = 1; i <= line; i++)
            {
                var captured = i;
                yield return C;
                C = C * (line - captured) / captured;
            }
        }
        return container.ToList();
    }

}

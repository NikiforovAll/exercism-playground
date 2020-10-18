using System;
using System.Collections.Generic;
using System.Linq;

public static class ListOps
{
    public static int Length<T>(List<T> input) => input.Count;

    public static List<T> Reverse<T>(List<T> input)
    {
        for (int i = 0; i < input.Count / 2; i++)
            (input[i], input[^(i + 1)]) = (input[^(i + 1)], input[i]);
        return input;
    }

    public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map)
    {
        return Map().ToList();
        IEnumerable<TOut> Map()
        {
            foreach (var item in input)
                yield return map(item);
        }
    }

    public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate)
    {
        return Filter().ToList();
        IEnumerable<T> Filter()
        {
            foreach (var item in input)
                if (predicate(item))
                    yield return item;
        }
    }

    public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func)
    {
        TOut res = start;
        foreach (var item in input)
        {
            res = func(res, item);
        }
        return res;
    }

    public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func)
    {
        TOut res = start;
        foreach (var item in Reverse(input))
        {
            res = func(item, res);
        }
        return res;
    }

    public static List<T> Concat<T>(List<List<T>> input) =>
        Foldl(input, new List<T>(), (l1, l2) => Append(l1, l2));

    public static List<T> Append<T>(List<T> left, List<T> right)
    {
        var length = left.Count + right.Count;
        var res = new List<T>(length);
        for (int i = 0; i < length; i++)
        {
            res.Add(i switch
            {
                var j when j < left.Count => left[j],
                _ => right[i - left.Count],
            });
        }
        return res;
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        if (string.IsNullOrEmpty(sequence))
            return ToAnswer((0, 0, 0, 0));

        ConcurrentBag<(int a, int c, int g, int t)> processedPartitions =
            new ConcurrentBag<(int a, int c, int g, int t)>();

        var partitions = Partitioner.Create(0, sequence.Length, rangeSize: 25);

        ParallelLoopResult plr = Parallel.ForEach(partitions, ParallelIteration);

        void ParallelIteration(Tuple<int, int> range, ParallelLoopState loopState)
        {
            var (start, end) = range;
            var chunk = sequence[start..end];
            // Console.WriteLine(sequence[start..end]);
            try
            {
                processedPartitions.Add(Count(chunk.AsSpan()));
            }
            catch (ArgumentException)
            {
                loopState.Stop();
            }

        }
        if (!plr.IsCompleted)
            throw new ArgumentException(nameof(Count));

        var accumulated = processedPartitions.Aggregate(
            (prev, next) => (prev.a + next.a, prev.c + next.c, prev.g + next.g, prev.t + next.t));
        return ToAnswer(accumulated);
    }

    private static IDictionary<char, int> ToAnswer((int a, int c, int g, int t) t) =>
        new Dictionary<char, int>
        {
            ['A'] = t.a,
            ['C'] = t.c,
            ['G'] = t.g,
            ['T'] = t.t,
        };
    private static (int a, int c, int g, int t) Count(ReadOnlySpan<char> source)
    {
        (int a, int c, int g, int t) result = (0, 0, 0, 0);
        foreach (var c in source)
        {
            switch (c)
            {
                case 'A':
                    result.a++;
                    break;
                case 'C':
                    result.c++;
                    break;
                case 'G':
                    result.g++;
                    break;
                case 'T':
                    result.t++;
                    break;
                default:
                    throw new ArgumentException(nameof(Count));
            }
        }
        return result;
    }

}

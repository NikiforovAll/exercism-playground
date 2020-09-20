using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class NucleotideCountV2
{
    public static IDictionary<char, int> Count(string sequence)
    {
        if (string.IsNullOrEmpty(sequence))
            return ToAnswer((0, 0, 0, 0));

        IDnaCounter engine = new ParallelDnaCounter(sequence);
        var result = engine.Run();

        return ToAnswer(result);
    }

    private static IDictionary<char, int> ToAnswer((int a, int c, int g, int t) tuple) =>
        new Dictionary<char, int>
        {
            ['A'] = tuple.a,
            ['C'] = tuple.c,
            ['G'] = tuple.g,
            ['T'] = tuple.t,
        };

    public interface IDnaCounter
    {
        (int a, int c, int g, int t) Run();
    }

    private class ParallelDnaCounter : IDnaCounter
    {
        private readonly string _sequence;
        private const int Chunk_Size = 25;
        public ParallelDnaCounter(string sequence) => _sequence = sequence;
        public (int a, int c, int g, int t) Run()
        {
            ConcurrentBag<(int a, int c, int g, int t)> processedPartitions =
                new ConcurrentBag<(int a, int c, int g, int t)>();

            var partitions = Partitioner.Create(0, _sequence.Length, rangeSize: Chunk_Size);

            ParallelLoopResult plr = Parallel.ForEach(partitions, (currentPartition, loopState) =>
            {
                var (start, end) = currentPartition;
                var chunk = _sequence[start..end];
                try
                {
                    processedPartitions.Add(Counter(chunk.AsSpan()));
                }
                catch (ArgumentException)
                {
                    loopState.Stop();
                }
            });

            if (!plr.IsCompleted)
                throw new ArgumentException(nameof(Run));

            return processedPartitions
                .Aggregate((prev, next) => (prev.a + next.a, prev.c + next.c, prev.g + next.g, prev.t + next.t));
        }

        private static (int a, int c, int g, int t) Counter(ReadOnlySpan<char> source)
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
                        throw new ArgumentException(nameof(Counter));
                }
            }
            return result;
        }
    }
}

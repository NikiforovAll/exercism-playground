using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Awaitable;

namespace Awaitable
{
    public static class LargestSeriesProductV2
    {
        public static long GetLargestProduct<T>(string digits, int span, TextWriter writer = default)
        {
            // while string is not ended
            // await expand
            // while curr element not zero
            // await slide

            RunLargestSeriesProductGuard();
            var source = ToDigits(digits).ToArray().AsMemory();
            var solver = new Solver
            {
                StdOut = writer
            };
            if (typeof(T) == typeof(MultipleSlidingWindowsStrategy))
            {
                var strategy = new MultipleSlidingWindowsStrategy(source, span);
                solver.RunDriver<MultipleSlidingWindowsStrategy, long>(strategy);
                return strategy.Result;
            }
            else if (typeof(T) == typeof(ChaosWindowStrategy))
            {
                var mid = source.Length / 2;
                var strategies = new SlidingWindowStrategy[]{
                    new SlidingWindowStrategy(source[..(mid + span)], span),
                    new SlidingWindowStrategy(source[(mid - span)..], span),
                };
                solver.RunDriver<SlidingWindowStrategy, long>(strategies);
                return strategies.Select(s => s.Result).Max();
            }
            else
            {
                var strategy = new SlidingWindowStrategy(source, span);
                solver.RunDriver<SlidingWindowStrategy, long>(strategy);
                return strategy.Result;
            }

            static IEnumerable<byte> ToDigits(string digits) =>
                digits.Select<char, byte>(
                    d => char.IsDigit(d)
                        ? (byte)(d - '0')
                        : throw new ArgumentException(nameof(ToDigits)));

            void RunLargestSeriesProductGuard()
            {
                if (digits == string.Empty && span > 0
                    || span < 0
                    || digits.Length < span)
                    throw new ArgumentException(nameof(RunLargestSeriesProductGuard));
            }
        }
    }
}

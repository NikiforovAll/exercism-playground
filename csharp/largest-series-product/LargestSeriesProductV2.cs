using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awaitable;

public static class LargestSeriesProductV2
{
    public static long GetLargestProduct(string digits, int span)
    {
        // while string is not ended
        // await expand
        // while curr element not zero
        // await slide


        RunLargestSeriesProductGuard();
        var source = ToDigits(digits);
        var root = new SlidingWindowStrategy(source, span);
        while (true)
        {
            root.Tick();
            if (root.Status != StrategyStatus.InProgress)
                break;
        }
        // var r = new StrategyTask(root).GetAwaiter().GetResult();
        return 0;

        static IList<byte> ToDigits(string digits) =>
            digits.Select<char, byte>(
                d => char.IsDigit(d)
                    ? (byte)(d - '0')
                    : throw new ArgumentException(nameof(ToDigits)))
                .ToList<byte>();

        void RunLargestSeriesProductGuard()
        {
            if (digits == string.Empty && span > 0
                || span < 0
                || digits.Length < span)
                throw new ArgumentException(nameof(LargestSeriesProduct));
        }
    }

}

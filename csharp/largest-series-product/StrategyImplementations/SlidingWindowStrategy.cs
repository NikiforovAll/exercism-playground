using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Awaitable
{
    public class SlidingWindowStrategy : Strategy
    {
        private readonly int span;

        public SlidingWindowStrategy(byte[] digits, int span)
        {
            Digits = digits;
            this.span = span;
        }

        public byte[] Digits { get; }

        protected override async StrategyTask<bool> Run()
        {
            bool success;
            long max = long.MinValue;
            var mem = Digits.ToArray().AsMemory();
            Range scanned = 0..0;
            do
            {
                var slide = new SlideStrategy(mem, scanned, span);
                success = await slide;
                if (success)
                {
                    var mul = RuntimeHelpers
                        .GetSubArray(Digits, scanned)
                        .Aggregate(1, (x, y) => x * y);
                    max = Math.Max(max, mul);
                }
                scanned = slide.Scanned;
            } while (success);

            return true;
        }
    }
}

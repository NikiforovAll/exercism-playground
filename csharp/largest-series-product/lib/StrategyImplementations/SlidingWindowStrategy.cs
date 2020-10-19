using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Awaitable
{
    public class SlidingWindowStrategy : Strategy<long>
    {
        private readonly int span;

        public SlidingWindowStrategy(ReadOnlyMemory<byte> digits, int span)
        {
            Digits = digits;
            this.span = span;
        }

        public ReadOnlyMemory<byte> Digits { get; }
        public long Result { get; internal set; }

        protected override async StrategyTask<long> Run()
        {
            bool success;
            long max = long.MinValue;
            Range scan = ..;
            var mem = Digits;
            do
            {
                var slide = new SlideStrategy(mem, scan, span);
                success = await slide && slide.Scanned.End.Value != 0;
                if (success)
                {
                    var mul = MultiplyDigits(mem[slide.Scanned]);
                    max = Math.Max(max, mul);
                }
                scan = slide.Scanned;
                mem = mem[scan.Start.Value..];
            } while (success);
            Result = max;
            return max;

            static long MultiplyDigits(ReadOnlyMemory<byte> digits)
            {
                var acc = 1;
                for (int i = 0; i < digits.Length; i++)
                {
                    acc *= digits.Span[i];
                }
                return acc;
            }
        }
    }
}

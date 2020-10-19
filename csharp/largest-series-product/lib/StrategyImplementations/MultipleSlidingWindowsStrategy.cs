using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Awaitable
{
    public class MultipleSlidingWindowsStrategy : Strategy<long>
    {
        private readonly int span;

        public MultipleSlidingWindowsStrategy(ReadOnlyMemory<byte> digits, int span)
        {
            Digits = digits;
            this.span = span;
        }

        public ReadOnlyMemory<byte> Digits { get; }
        public long Result { get; internal set; }

        protected override async StrategyTask<long> Run()
        {
            var mid = Digits.Length / 2;
            var strat1 = new SlidingWindowStrategy(Digits[..(mid + span)], 2);
            var start2 = new SlidingWindowStrategy(Digits[(mid - span)..], 2);
            await WhenAll(strat1, start2);

            var res = Math.Max(strat1.Result, start2.Result);
            Result = res;
            return res;
        }
        protected StrategyTask WhenAll(params IStrategy[] strategies)
        {
            return new StrategyTask(strategies);
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Awaitable
{
    internal class ShiftStrategy : Strategy
    {
        private readonly Memory<byte> source;
        private readonly int span;

        public ShiftStrategy(Memory<byte> source, int span)
        {
            this.source = source;
            this.span = span;
        }
        public Range Scanned;
        protected override async StrategyTask<bool> Run()
        {
            if (source.Span[span] == 0)
                return false;
            Scanned = 1..(span + 1);
            return true;
        }
    }
}

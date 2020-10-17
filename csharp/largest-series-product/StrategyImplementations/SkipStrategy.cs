using System;
using System.Threading.Tasks;

namespace Awaitable
{
    internal class SkipStrategy : Strategy
    {
        private Memory<byte> source;

        public SkipStrategy(Memory<byte> source)
        {
            this.source = source;
        }
        public Range Scanned;
        protected override async StrategyTask<bool> Run()
        {
            var cnt = 0;
            while (source.Span[cnt] == 0)
            {
                Scanned = cnt++..;
            }
            return Scanned.End.Value < source.Length;

        }
    }
}

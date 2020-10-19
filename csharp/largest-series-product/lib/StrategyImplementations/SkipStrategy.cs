using System;
using System.Threading.Tasks;
#pragma warning disable CS1998

namespace Awaitable
{
    internal class SkipStrategy : Strategy<bool>
    {
        private readonly ReadOnlyMemory<byte> source;

        public SkipStrategy(ReadOnlyMemory<byte> source)
        {
            this.source = source;
        }
        public Range Scanned;
        protected override async StrategyTask<bool> Run()
        {
            var cnt = 0;
            var wasSkipped = false;
            while (source.Span[cnt] == 0)
            {
                wasSkipped = true;
                Scanned = ++cnt..;
            }
            return wasSkipped;
        }
    }
}

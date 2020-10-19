using System;
using System.Threading.Tasks;

namespace Awaitable
{
    internal class ShiftStrategy : Strategy<bool>
    {
        private readonly ReadOnlyMemory<byte> source;
        private readonly int span;

        public ShiftStrategy(ReadOnlyMemory<byte> source, int span)
        {
            this.source = source;
            this.span = span;
        }
        public Range Scanned;

        /// <summary>
        /// Next shifted window
        /// </summary>
        /// <returns><see langword="false"/> if operation not possible</returns>
        protected override async StrategyTask<bool> Run()
        {
            if (span >= source.Length)
            {
                Scanned = default;
                return false;
            }

            if (source.Span[span] == 0)
            {
                var expand = new ExpandStrategy(source[(span + 1)..], span);
                var expanded = await expand;
                var shiftedRange = (expand.Scanned.Start.Value + span)..(expand.Scanned.End.Value + span);
                Scanned = shiftedRange;
                return expanded;
            }
            Scanned = 1..(span + 1);
            return true;
        }
    }
}

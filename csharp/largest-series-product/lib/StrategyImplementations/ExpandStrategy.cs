using System;
using System.Collections.Generic;
using System.Linq;

namespace Awaitable
{
    public class ExpandStrategy : Strategy<bool>
    {
        private readonly int span;

        public ExpandStrategy(ReadOnlyMemory<byte> source, int span)
        {
            this.source = source;
            this.span = span;
        }

        private readonly ReadOnlyMemory<byte> source;

        public Range Scanned;

        /// <summary>
        /// Next expanded window
        /// </summary>
        /// <returns><see langword="false"/> if operation not possible</returns>
        protected override async StrategyTask<bool> Run()
        {
            var end = 0;
            var start = 0;
            while (end < source.Length && (end - start) < span)
            {
                if (source.Span[end] == 0)
                {
                    var skip = new SkipStrategy(source);
                    var skipped = await skip;
                    start = skip.Scanned.Start.Value;
                    if (!skipped)
                        return false;
                }
                Scanned = start..(end + 1);
                end++;
            }
            return true;
        }
    }
}

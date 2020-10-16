using System;
using System.Collections.Generic;
using System.Linq;

namespace Awaitable
{
    public class ExpandStrategy : Strategy
    {
        private readonly int span;

        public ExpandStrategy(Memory<byte> source, int span)
        {
            Source = source;
            this.span = span;
        }

        public Memory<byte> Source { get; }

        public Range Scanned;

        protected override async StrategyTask<bool> Run()
        {
            var end = 0;
            var start = 0;
            while (end < Source.Length && (end - start) < span)
            {
                if (Source.Span[end] == 0)
                {
                    var skip = new SkipStrategy(Source);
                    var skipped = await skip;
                    start = skip.Scanned.Start.Value;
                    if (!skipped)
                        return false;
                }
                Scanned = start..end;
                end++;
            }
            return true;
        }
    }
}

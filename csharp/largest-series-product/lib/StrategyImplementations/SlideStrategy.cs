using System;
using System.Collections.Generic;
using System.Linq;

namespace Awaitable
{
    public class SlideStrategy : Strategy<bool>
    {
        private readonly ReadOnlyMemory<byte> source;
        private readonly int span;

        public SlideStrategy(ReadOnlyMemory<byte> source, Range scanned, int span)
        {
            this.source = source;
            Scanned = scanned;
            this.span = span;
        }


        public Range Scanned;

        protected override async StrategyTask<bool> Run()
        {
            var skipped = false;
            var newSource = Scanned;
            if (source.Span[0] == 0)
            {
                var skip = new SkipStrategy(source);
                skipped = await skip;
                Scanned = skip.Scanned;
                if (skipped && newSource.End.Value == 0 && !newSource.End.IsFromEnd)
                {
                    return false;
                }
            }
            if (Scanned.End.Value - Scanned.Start.Value != span)
            {
                var expand = new ExpandStrategy(source[newSource.Start.Value..], span);
                var expanded = await expand;
                Scanned = expand.Scanned;
                return expanded;
            }
            else
            {
                var shift = new ShiftStrategy(source, span);
                var shifted = await shift;
                Scanned = shift.Scanned;
                return shifted;
            }
        }
    }
}

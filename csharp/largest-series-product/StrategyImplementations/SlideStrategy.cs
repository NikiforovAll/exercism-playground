using System;
using System.Collections.Generic;
using System.Linq;

namespace Awaitable
{
    public class SlideStrategy : Strategy
    {
        private readonly Memory<byte> source;
        private readonly int span;

        public SlideStrategy(Memory<byte> source, Range scanned, int span)
        {
            this.source = source;
            Scanned = scanned;
            this.span = span;
        }


        public Range Scanned;

        protected override async StrategyTask<bool> Run()
        {
            if (Scanned.End.Value - Scanned.Start.Value + 1 != span)
            {
                var expand = new ExpandStrategy(source, span);
                var expanded = await expand;
                Scanned = expand.Scanned;
                return expanded;
            }
            else
            {
                var shift = new ShiftStrategy(source, span);
                var shifted = await shift;
                if (!shifted)
                {
                    Scanned = shift.Scanned;
                    var expand = new ExpandStrategy(source[Scanned], span);
                    var expanded = await expand;
                    Scanned = expand.Scanned;
                    return expanded;
                }
                return true;
            }
        }
    }
}

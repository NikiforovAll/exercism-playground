using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class AccumulateExtensions
{
    public static IEnumerable<TOut> Accumulate<TIn, TOut>(
        this IEnumerable<TIn> collection,
        Func<TIn, TOut> func) => new MapEnumerable<TIn, TOut>(collection, func);

    private class MapEnumerable<TIn, TOut> : IEnumerable<TOut>
    {
        private MapEnumerator map;
        public MapEnumerable(IEnumerable<TIn> @internal, Func<TIn, TOut> projection) => 
            map = new MapEnumerator(@internal, projection);

        public IEnumerator<TOut> GetEnumerator() => map;
        IEnumerator IEnumerable.GetEnumerator() => map;

        private class MapEnumerator : IEnumerator<TOut>
        {
            private readonly IEnumerator<TIn> source;
            private readonly Func<TIn, TOut> projection;

            public MapEnumerator(IEnumerable<TIn> source, Func<TIn, TOut> projection)
            {
                this.source = source.GetEnumerator();
                this.projection = projection;
            }

            public TOut Current => Project(source.Current);

            object IEnumerator.Current => Project(source.Current);

            public void Dispose() => source.Dispose();
            public bool MoveNext() => source.MoveNext();
            public void Reset() => source.Reset();

            private TOut Project(TIn item) => projection(item);
        }
    }

    public static IEnumerable<U> AccumulateForbidden<T, U>(
        this IEnumerable<T> collection,
        Func<T, U> func) => collection.Select(func);
}

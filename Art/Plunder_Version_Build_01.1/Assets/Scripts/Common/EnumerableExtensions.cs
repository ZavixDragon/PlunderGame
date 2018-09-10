using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Code.Common
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> source)
        {
            return source.Shuffle().First();
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }
}

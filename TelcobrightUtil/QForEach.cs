using System;
using System.Collections.Generic;
using System.Text;

namespace TelcobrightUtil
{
    public static class QForEach
    {
        public static void ForEach<T>(this IEnumerable<T> e, Action<T> cb)
        {
            foreach (var item in e) cb(item);
        }
        public static void ForEach<T>(this IEnumerable<T> e, Action<T, int> cb)
        {
            var index = 0;
            foreach (var item in e) cb(item, index++);
        }
    }
}

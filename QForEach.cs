using System;
using System.Collections.Generic;
using System.Text;

namespace sms_portal_gwp
{
    public static class QForEach
    {
        public static void ForEach<T>(this IEnumerable<T> e, Action<T, int> cb)
        {
            var index = 0;
            foreach (var item in e) cb(item, index);
        }
    }
}

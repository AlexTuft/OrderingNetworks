using System.Collections.Generic;

namespace UpdatingFactories
{
    public static class ListExtensions
    {
        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var t = list[i];
            list[i] = list[j];
            list[j] = t;
        }
    }
}

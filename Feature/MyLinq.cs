using System.Collections.Generic;

namespace Feature
{
    public static class MyLinq
    {
        public static int Count<T>(this IEnumerable<T> sequense)
        {
            var count = 0;

            foreach (var item in sequense)
            {
                count += 1;
            }
            return count;
        }
    }
}
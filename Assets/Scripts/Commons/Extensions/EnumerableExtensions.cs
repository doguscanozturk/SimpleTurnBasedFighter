using System.Collections.Generic;
using System.Linq;

namespace Commons.Extensions
{
    public static class EnumerableExtensions
    {
        public static T GetRandomElement<T>(this IEnumerable<T> enumerable)
        {
            var randomIndex = UnityEngine.Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(randomIndex);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVUtils
{
    public static class Extensions
    {
        public static T[][] ToJaggedArray<T>(this IEnumerable<IEnumerable<T>> enumerable)
        {
            return enumerable.Select(each => each.ToArray()).ToArray();
        }
    }
}

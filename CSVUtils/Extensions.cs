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

        public static T[,] ToMultidimensionalArray<T>(this IEnumerable<IEnumerable<T>> enumerable)
        {
            T[,] result = new T[enumerable.Count(), enumerable.Select(each => each.Count()).Max()];
            int i = 0;
            int j = 0;
            foreach (IEnumerable<T> row in enumerable)
            {
                foreach (T item in row)
                {
                    result[i, j] = item;
                    j++;
                }
                i++;
                j = 0;
            }
            return result;
        }
    }
}

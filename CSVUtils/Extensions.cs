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

        public static IEnumerable<IEnumerable<T>> ToEnumerable<T>(this T[,] array)
        {
            List<List<T>> result = new List<List<T>>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                List<T> row = new List<T>();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    row.Add(array[i, j]);
                }
                result.Add(row);
            }
            return result;
        }


    }
}

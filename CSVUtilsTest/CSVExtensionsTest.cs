using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSVUtils;
using System.Collections.Generic;

namespace CSVTest
{
    [TestClass]
    public class CSVExtensionsTest
    {
        [TestMethod]
        public void FromEnumerableToJaggedArrayTest()
        {
            List<List<int>> original = new List<List<int>>()
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 4, 5, 6 }
            };
            int[][] expected = new int[][]
            {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 }
            };
            int[][] actual = original.ToJaggedArray();
            CSVAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FromJaggedArrayToMultidimensionalArrayTest()
        {
            int[][] original = new int[][]
            {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 }
            };
            int[,] expected = new int[2, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };
            int[,] actual = original.ToMultidimensionalArray();
            CSVAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FromMultidimensionalArrayToEnumerable()
        {
            int[,] original = new int[2, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };
            IEnumerable<IEnumerable<int>> expected = new List<List<int>>()
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 4, 5, 6 }
            };
            IEnumerable<IEnumerable<int>> actual = original.ToEnumerable();
            CSVAssert.AreEqual(expected.ToJaggedArray(), actual.ToJaggedArray());
        }
    }
}

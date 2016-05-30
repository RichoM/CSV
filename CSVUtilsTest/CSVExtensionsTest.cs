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
        public void ToJaggedArrayTest()
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
        public void ToMultidimensionalArrayTest()
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
    }
}

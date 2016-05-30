using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSVTest
{
    internal class CSVAssert
    {
        public static void AreEqual<T>(T[][] expected, T[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].Length, actual[i].Length);
                for (int j = 0; j < expected[i].Length; j++)
                {
                    Assert.AreEqual(expected[i][j], actual[i][j]);
                }
            }
        }
        
        public static void AreEqual<T>(T[,] expected, T[,] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected.GetLength(0), actual.GetLength(0));
            Assert.AreEqual(expected.GetLength(1), actual.GetLength(1));
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }
    }
}

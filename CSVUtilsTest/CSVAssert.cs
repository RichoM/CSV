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
    }
}

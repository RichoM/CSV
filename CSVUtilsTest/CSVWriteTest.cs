using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSVUtils;
using System.Linq;
using System.IO;

namespace CSVTest
{
    [TestClass]
    public class CSVWriteTest
    {
        [TestMethod]
        public void WritingSimpleCellsCanBeReadBackCorrectly()
        {
            string[][] expected = new string[][]
            {
                new string[] { "first", "second" },
                new string[] { "third", "fourth" }
            };
            string text = CSV.WriteString(expected);
            string[][] actual = CSV.ReadString(text).ToJaggedArray();
            CSVAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WritingEnclosedCellsCanBeReadBackCorrectly()
        {
            string[][] expected = new string[][]
            {
                new string[] { "first\r\nsecond", "second\nthird" },
                new string[] { "third \"fourth\"", "fourth, fifth" }
            };
            string text = CSV.WriteString(expected);
            string[][] actual = CSV.ReadString(text).ToJaggedArray();
            CSVAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DifferentSeparatorIsSupported()
        {
            string[][] expected = new string[][]
            {
                new string[] { "first\r\nsecond", "second\nthird" },
                new string[] { "third \"fourth\"", "fourth| fifth" }
            };
            string text = CSV.WriteString(expected, '|');
            string[][] actual = CSV.ReadString(text, '|').ToJaggedArray();
            CSVAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WritingToAFileIsSupported()
        {
            string fileName = Path.GetTempFileName();
            try
            {
                string[][] expected = new string[][]
                {
                new string[] { "first\r\nsecond", "second\nthird", "something else" },
                new string[] { "third \"fourth\"", "fourth, fifth", "I don't know\t\t\r\n\n\n\r\n" }
                };
                CSV.WriteFile(expected, fileName, ';');
                string[][] actual = CSV.ReadFile(fileName, ';').ToJaggedArray();
                CSVAssert.AreEqual(expected, actual);
            }
            finally
            {
                File.Delete(fileName);
            }
        }
                
    }
}

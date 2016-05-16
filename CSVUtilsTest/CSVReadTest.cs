using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSVUtils;
using System.Globalization;

namespace CSVTest
{
    [TestClass]
    public class CSVReadTest
    {
        [TestMethod]
        public void ParserCountsASingleRowCorrectly()
        {
            string s = "abc,def,ghi";
            Assert.AreEqual(1, CSV.ReadString(s).Count());
        }

        [TestMethod]
        public void ParserCountsSeveralRowsCorrectly()
        {
            string s = "abc,def,ghi\njkl,mnñ,opq\nrst,uvw,xyz";
            Assert.AreEqual(3, CSV.ReadString(s).Count());
        }

        [TestMethod]
        public void ParserWorksWithBothLineSeparators()
        {
            string s = "abc\ndefg\n\rhij";
            Assert.AreEqual(3, CSV.ReadString(s).Count());
        }

        [TestMethod]
        public void ParserReadsSingleRowCorrectly()
        {
            string s = "abc,def,ghi";
            string[] row = CSV.ReadString(s).ElementAt(0);
            Assert.AreEqual(3, row.Length);
            Assert.AreEqual("abc", row[0]);
            Assert.AreEqual("def", row[1]);
            Assert.AreEqual("ghi", row[2]);
        }

        [TestMethod]
        public void ParserReadsSeveralRowsCorrectly()
        {
            string s = "abc,def\nghi,jkl\nmnñ,opq";
            string[][] rows = CSV.ReadString(s).ToArray();
            string[][] expected = new string[][]
            {
                new string[] { "abc", "def" },
                new string[] { "ghi", "jkl" },
                new string[] { "mnñ", "opq" }
            };
            AssertEqualCSV(expected, rows);
        }

        [TestMethod]
        public void ParserReadsEnclosedRowsCorrectly()
        {
            string s = "\"abc,def\"\n\"ghi,jkl\"\n\"mnñ,opq\"";
            string[][] rows = CSV.ReadString(s).ToArray();
            string[][] expected = new string[][]
            {
                new string[] { "abc,def" },
                new string[] { "ghi,jkl" },
                new string[] { "mnñ,opq" }
            };
            AssertEqualCSV(expected, rows);
        }

        [TestMethod]
        public void EnclosedRowsCanContainNewLines()
        {
            string s = "\"ab\ncd\",ef\nkl,\"gh\r\nij\"\n\"mn,ño\",\"\r\n\"";
            string[][] rows = CSV.ReadString(s).ToArray();
            string[][] expected = new string[][]
            {
                new string[] { "ab\ncd", "ef" },
                new string[] { "kl", "gh\r\nij" },
                new string[] { "mn,ño", "\r\n" }
            };
            AssertEqualCSV(expected, rows);
        }

        [TestMethod]
        public void ParserAcceptsAnyCharAsDelimiter()
        {
            string s = "a|b|c\nd|e|f";
            string[][] rows = CSV.ReadString(s, '|').ToArray();
            string[][] expected = new string[][]
            {
                new string[] { "a", "b", "c" },
                new string[] { "d", "e", "f" },
            };
            AssertEqualCSV(expected, rows);            
        }

        [TestMethod]
        public void EnclosedCellsCanHaveEscapedQuotationMarks()
        {
            string s = "\"\"\"a\"\"\",b,c\nd,\"\"\"e\"\"\",f";
            string[][] rows = CSV.ReadString(s, ',').ToArray();
            string[][] expected = new string[][]
            {
                new string[] { @"""a""", "b", "c" },
                new string[] { "d", @"""e""", "f" }
            };
            AssertEqualCSV(expected, rows);
        }

        [TestMethod]
        public void ParserCanAlsoReadFiles()
        {
            string[][] rows = CSV.ReadFile("tweets.csv").ToArray();
            Assert.AreEqual(4336, rows.Length); // Same # of rows
            Assert.IsTrue(rows.All(row => row.Length == 14)); // Same # of columns
            Assert.AreEqual("idTweets", rows[0][0]); // First cell is correct
            Assert.AreEqual("0", rows[4335][13]); // Last cell is correct

            // Total # of followers is correct
            int totalFollowers = rows.Where((row, index) => index > 0)
                .Select(row => int.Parse(row[12]))
                .Sum();
            Assert.AreEqual(23589979, totalFollowers);

            for (int i = 2; i < rows.Length; i++)
            {
                // IDs are ordered
                Assert.IsTrue(long.Parse(rows[i][0]) > long.Parse(rows[i - 1][0]));

                // Date and time are also ordered
                Func<string, string, DateTime> asDateTime = (date, time) =>
                {
                    string str = string.Format("{0} {1}", date, time);
                    return DateTime.ParseExact(str, "M/d/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                };
                DateTime dt1 = asDateTime(rows[i - 1][2], rows[i - 1][3]);
                DateTime dt2 = asDateTime(rows[i][2], rows[i][3]);
                Assert.IsTrue(dt2 >= dt1);
            }
        }

        private void AssertEqualCSV(string[][] expected, string[][] actual)
        {
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i].Length, actual[i].Length);
                for (int j = 0; j < actual[i].Length; j++)
                {
                    Assert.AreEqual(expected[i][j], actual[i][j]);
                }
            }
        }
    }
}

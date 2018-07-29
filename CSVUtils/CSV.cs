using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Streams;

namespace CSVUtils
{
    public class CSV
    {
        public static IEnumerable<IEnumerable<string>> ReadString(string str, char separator = ',')
        {
            using (CSVReader csv = new CSVReader(new TextStream(str), separator))
            {
                return csv.Rows;
            }
        }

        public static IEnumerable<IEnumerable<string>> ReadFile(FileInfo file, char separator = ',', Encoding encoding = null)
        {
            using (CSVReader csv = new CSVReader(new TextStream(file.OpenRead(), encoding ?? Encoding.Default), separator))
            {
                return csv.Rows;
            }
        }

        public static IEnumerable<IEnumerable<string>> ReadFile(string fileName, char separator = ',', Encoding encoding = null)
        {
            return ReadFile(new FileInfo(fileName), separator, encoding);
        }

        public static string WriteString(
            IEnumerable<IEnumerable<string>> rows,
            char separator = ',')
        {
            string result = null;
            StringWriter writer = null;
            using (CSVWriter csv = new CSVWriter(writer = new StringWriter(), separator))
            {
                csv.WriteRows(rows);
                result = writer.ToString();
            }
            return result;
        }

        public static void WriteFile(
            IEnumerable<IEnumerable<string>> rows, 
            FileInfo file, 
            char separator = ',',
            bool overwrite = true,
            Encoding encoding = null)
        {
            WriteFile(rows, file.FullName, separator, overwrite, encoding);
        }

        public static void WriteFile(
            IEnumerable<IEnumerable<string>> rows,
            string fileName,
            char separator = ',',
            bool overwrite = true,
            Encoding encoding = null)
        {
            using (CSVWriter csv = new CSVWriter(new StreamWriter(fileName, !overwrite, encoding ?? Encoding.Default), separator))
            {
                csv.WriteRows(rows);
            }
        }
    }
}

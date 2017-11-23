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

        public static IEnumerable<IEnumerable<string>> ReadFile(FileInfo file, char separator = ',')
        {
            using (CSVReader csv = new CSVReader(new TextStream(file.OpenText()), separator))
            {
                return csv.Rows;
            }
        }

        public static IEnumerable<IEnumerable<string>> ReadFile(string fileName, char separator = ',')
        {
            return ReadFile(new FileInfo(fileName), separator);
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
            bool overwrite = true)
        {
            WriteFile(rows, file.FullName, separator, overwrite);
        }

        public static void WriteFile(
            IEnumerable<IEnumerable<string>> rows,
            string fileName,
            char separator = ',',
            bool overwrite = true)
        {
            using (CSVWriter csv = new CSVWriter(new StreamWriter(fileName, !overwrite), separator))
            {
                csv.WriteRows(rows);
            }
        }
    }
}

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
        public static IEnumerable<string[]> ReadString(string str, char separator = ',')
        {
            using (CSVReader csv = new CSVReader(new TextStream(str), separator))
            {
                return csv.Rows;
            }
        }

        public static IEnumerable<string[]> ReadFile(FileInfo file, char separator = ',')
        {
            using (CSVReader csv = new CSVReader(new TextStream(file.OpenText()), separator))
            {
                return csv.Rows;
            }
        }

        public static IEnumerable<string[]> ReadFile(string fileName, char separator = ',')
        {
            return ReadFile(new FileInfo(fileName), separator);
        }
    }
}

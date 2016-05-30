using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVUtils
{
    internal class CSVWriter : IDisposable
    {
        private TextWriter writer;
        private char separator;

        public CSVWriter(TextWriter writer, char separator = ',')
        {
            this.writer = writer;
            this.separator = separator;
        }

        public void WriteRow(IEnumerable<string> row)
        {
            bool first = true;
            foreach (string cell in row)
            {
                if (!first) { writer.Write(separator); }
                first = false;
                string text = cell.Replace("\"", "\"\""); // Escape quotes, if any
                bool enclose = text.Contains(separator)
                            || text.Contains('"')
                            || text.Contains('\n')
                            || text.Contains('\r');
                if (enclose) { writer.Write('"'); }
                writer.Write(text);
                if (enclose) { writer.Write('"'); }
            }
            writer.WriteLine();
            writer.Flush();
        }

        public void WriteRows(IEnumerable<IEnumerable<string>> rows)
        {
            foreach (IEnumerable<string> row in rows)
            {
                WriteRow(row);
            }
        }

        public void Dispose()
        {
            writer.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Streams;

namespace CSVUtils
{
    internal class CSVReader : IDisposable
    {
        private TextStream stream;
        private char delimiter;

        public CSVReader(TextStream stream, char delimiter)
        {
            this.stream = stream;
            this.delimiter = delimiter;
        }

        public IEnumerable<string[]> Rows
        {
            get
            {
                return Parse();
            }
        }

        private IEnumerable<string[]> Parse()
        {
            List<string[]> rows = new List<string[]>();
            while (!stream.AtEnd)
            {
                rows.Add(NextRow());                
                while ("\r\n".Contains(stream.Peek())) { stream.Skip(); }
            }
            return rows;
        }

        private string[] NextRow()
        {
            List<string> cells = new List<string>();
            while (!stream.AtEnd && !"\r\n".Contains(stream.Peek()))
            {
                cells.Add(NextCell());
                if (delimiter.Equals(stream.Peek()))
                {
                    stream.Skip();
                }
            }
            return cells.ToArray();
        }

        private string NextCell()
        {
            if ('"'.Equals(stream.Peek()))
            {
                return NextEnclosedCell();
            }
            else
            {
                return NextNormalCell();
            }
        }

        private string NextEnclosedCell()
        {
            StringBuilder sb = new StringBuilder();
            stream.Skip();
            while (!stream.AtEnd)
            {
                char next = stream.Next();
                if ('"'.Equals(next))
                {
                    if ('"'.Equals(stream.Peek()))
                    {
                        stream.Skip();
                    }
                    else
                    {
                        break;
                    }
                }
                sb.Append(next);
            }
            return sb.ToString();
        }

        private string NextNormalCell()
        {
            StringBuilder sb = new StringBuilder();
            while (!stream.AtEnd)
            {
                char next = stream.Peek();
                if (delimiter.Equals(next) ||
                    '\n'.Equals(next) ||
                    '\r'.Equals(next))
                {
                    break;
                }
                sb.Append(stream.Next());
            }
            return sb.ToString();
        }

        public void Dispose()
        {
            stream.Dispose();
        }
    }
}

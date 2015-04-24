using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser
{
    public static class ParserBuilder
    {
        public static IParser Create(string path)
        {
            return Create(path, ParserSettings.CSV);
        }

        public static IParser Create(string path, ParserSettings parserSettings)
        {
            return Create(
                new FileStream(".\\MOCK_DATA.csv", FileMode.Open, FileAccess.Read, FileShare.None, 4096),
                parserSettings);
        }

        public static IParser Create(Stream stream, ParserSettings parserSettings)
        {
            return Create(
                new StreamReader(stream),
                parserSettings);
        }

        public static IParser Create(StreamReader reader, ParserSettings parserSettings)
        {
            return new LineEnumerable(reader, parserSettings);
        }
    }
}

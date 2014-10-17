/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using System.Collections.Generic;
using System.IO;

namespace FileParser
{
    public class LineEnumerable : IEnumerable<IEnumerable<string>>
    {
        readonly StreamReader _reader;
        readonly ParserSettings _settings;

        public LineEnumerable(StreamReader reader, ParserSettings settings)
        {
            _reader = reader;
            _settings = settings;
        }

        public IEnumerator<IEnumerable<string>> GetEnumerator()
        {
            return new LineEnumerator(_reader, _settings);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

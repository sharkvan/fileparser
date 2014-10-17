/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using System.Collections.Generic;
using System.IO;
using System;

namespace FileParser
{
    public class LineEnumerator : IEnumerator<IEnumerable<string>>
    {
        StreamReader _reader;
        IEnumerable<string> _current;
        readonly ParserSettings _settings;

        public LineEnumerator(StreamReader reader, ParserSettings settings)
        {
            _reader = reader;
            _settings = settings;
        }

        public IEnumerable<string> Current
        {
            get { return _current; }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            _reader = null;
        }

        public bool MoveNext()
        {
            if (_reader.EndOfStream)

                return false;

            _current = new FieldEnumerable(_reader, _settings);

            return true;
        }

        public void Reset()
        {
            if (_reader.BaseStream.CanSeek)
                _reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }
    }
}

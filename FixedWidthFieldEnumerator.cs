/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using FileParser.PatternsAndAlgorithms;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileParser
{
    internal class FixedWidthFieldEnumerator : IEnumerator<string>
    {
        StreamReader _reader;
        IEnumerator<int> _fieldSizes;
        char[] _buffer = new char[1024];
        int _length = 0;

        public FixedWidthFieldEnumerator(StreamReader reader, ParserSettings settings)
        {
            _reader = reader;
            _fieldSizes = settings.FieldSizes.GetEnumerator();
        }

        public string Current
        {
            get 
            { 
                return new string(_buffer, 0, _length); 
            }
        }

        public void Dispose()
        {
            _reader = null;
            Pattern.DisposeClass(ref _fieldSizes);
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (_fieldSizes.MoveNext())
            {
                if (_fieldSizes.Current < _buffer.Length)
                    Array.Resize(ref _buffer, _fieldSizes.Current);

                _length = _reader.Read(_buffer, 0, _fieldSizes.Current);

                return _length > 0;
            }

            return false;
        }

        public void Reset()
        {
            if (_reader.BaseStream.CanSeek)
            {
                _reader.BaseStream.Seek(0, SeekOrigin.Begin);
                _fieldSizes.Reset();
            }
        }
    }
}
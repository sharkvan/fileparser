/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FileParser.CharStateReader;

namespace FileParser
{
    internal class DelimitedFieldEnumerator : IEnumerator<string>
    {
        StreamReader _reader;
        StateReader _currentState;
        char[] _buffer;
        int _length;

        readonly InitialState INITIAL_STATE;

        public DelimitedFieldEnumerator(StreamReader reader, ParserSettings settings)
        {
            _reader = reader;
            _buffer = new char[1024];
            _length = 0;

            INITIAL_STATE = new InitialState(settings.FieldDelimiter, settings.TextQualifier);
            _currentState = INITIAL_STATE;
        }

        public string Current
        {
            get { return new string(_buffer, 0, _length); }
        }

        public void Dispose()
        {
            _reader = null;
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (_reader.EndOfStream || !_currentState.ContinueReadingLine)
                return false;

            _currentState = INITIAL_STATE;
            _length = 0;

            while (!_currentState.IsEndState)
            {
                if (_length == _buffer.Length - 1)
                    Array.Resize(ref _buffer, (int)(_length * 1.1));

                _currentState = _currentState.Read(_reader, ref _buffer, ref _length);
            }

            return true;
        }

        public void Reset()
        {
            if (_reader.BaseStream.CanSeek)
            {
                _reader.BaseStream.Seek(0, SeekOrigin.Begin);
                _currentState = INITIAL_STATE;
                _length = 0;
                _buffer = new char[1024];
            }
        }
    }
}
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

namespace FileParser.CharStateReader
{
    internal sealed class EndTextQualifierState : StateReader
    {
        readonly char _readTill;
        readonly StateReader _nextState;

        public EndTextQualifierState(char readTill, StateReader nextState)
        {
            _readTill = readTill;
            _nextState = nextState;
        }

        public override StateReader Read(StreamReader reader, ref char[] buffer, ref int bufferPosition)
        {
            if (reader.EndOfStream)
                throw new ApplicationException("Missing closing text qualifier");

            return ReadChar(reader.Read(), ref buffer, ref bufferPosition);
        }

        public override StateReader ReadChar(int c, ref char[] buffer, ref int bufferPosition)
        {
            if (_readTill == c)
                return _nextState;

            buffer[bufferPosition++] = (char)c;

            return this;
        }
    }
}

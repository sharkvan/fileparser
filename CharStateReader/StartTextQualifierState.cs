/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileParser.CharStateReader
{
    internal sealed class StartTextQualifierState : StateReader
    {
        readonly char _readTill;
        readonly StateReader _nextState;

        public StartTextQualifierState(char readTill, StateReader nextState)
        {
            _readTill = readTill;
            _nextState = new EndTextQualifierState(readTill, nextState);
        }

        public char LookingFor { get { return _readTill; } }

        public override StateReader ReadChar(int c, ref char[] buffer, ref int bufferPosition)
        {
            if (_readTill == c)
                return _nextState;

            buffer[bufferPosition++] = (char)c;

            return this;
        }
    }
}

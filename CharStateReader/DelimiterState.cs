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
    internal sealed class DelimiterState : StateReader
    {
        readonly char _readTill;
        readonly StateReader _nextState;
        readonly NewLinePatternState _newLinePatternState;

        public DelimiterState(char readTill, StateReader nextState)
        {
            _readTill = readTill;
            _nextState = nextState;
            _newLinePatternState = new NewLinePatternState(this);
        }

        public override StateReader ReadChar(int c, ref char[] buffer, ref int bufferPosition)
        {
            if (_readTill == c)
                return _nextState;

            return _newLinePatternState.ReadChar(c, ref buffer, ref bufferPosition);
        }
    }
}

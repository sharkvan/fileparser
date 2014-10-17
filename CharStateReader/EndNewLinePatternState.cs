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
    internal sealed class EndNewLinePatternState : EndState
    {
        readonly char _lookFor;
        readonly StateReader _returnToState;

        public override bool IsEndState
        {
            get
            {
                return !ContinueReadingLine;
            }
        }

        public EndNewLinePatternState(char c, StateReader returnToState)
        {
            _lookFor = c;
            _returnToState = returnToState;
        }

        public override StateReader ReadChar(int c, ref char[] buffer, ref int bufferPosition)
        {
            if (_lookFor == c)
            {
                StopReadingLine();
                return this;
            }

            buffer[bufferPosition++] = (char)c;

            return _returnToState;
        }
    }
}

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
    internal sealed class NewLinePatternState : StateReader
    {
        readonly char _lookFor;
        readonly StateReader _returnToState;
        readonly StateReader _nextState;

        public NewLinePatternState(StateReader returnToState) : this(Environment.NewLine.ToCharArray(), 0, returnToState) { }
        private NewLinePatternState(char[] chars, int indexToUse, StateReader returnToState)
        {
            _lookFor = chars[indexToUse++];
            _returnToState = returnToState;

            if (indexToUse < chars.Length - 1)
                _nextState = new NewLinePatternState(chars, indexToUse, _returnToState);

            _nextState = new EndNewLinePatternState(chars[indexToUse], _returnToState);
        }

        public override StateReader ReadChar(int c, ref char[] buffer, ref int bufferPosition)
        {
            if ('\n' == c)
                return new EndNewLinePatternState('\n', _returnToState).ReadChar(c, ref buffer, ref bufferPosition);

            if (_lookFor == c)
                return _nextState;

            buffer[bufferPosition++] = (char)c;

            return _returnToState;
        }
    }
}

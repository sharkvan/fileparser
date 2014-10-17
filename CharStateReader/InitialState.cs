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
    internal sealed class InitialState : StateReader
    {
        readonly DelimiterState _delimiterState;
        readonly StartTextQualifierState _qualifiedTextState;

        public InitialState(char delimiter, char textQualifier)
        {
            _delimiterState = new DelimiterState(delimiter, _endState);
            _qualifiedTextState = new StartTextQualifierState(textQualifier, _delimiterState);
        }

        public override StateReader ReadChar(int c, ref char[] buffer, ref int bufferPosition)
        {
            if (_qualifiedTextState.LookingFor == c)
                return _qualifiedTextState.ReadChar(c, ref buffer, ref bufferPosition);

            return _delimiterState.ReadChar(c, ref buffer, ref bufferPosition);
        }
    }
}

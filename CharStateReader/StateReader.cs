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
    internal abstract class StateReader
    {
        protected readonly static EndState _endState = new EndState();
        private bool _continueReadingLine = true;

        public virtual StateReader Read(StreamReader reader, ref char[] buffer, ref int bufferPosition)
        {
            if (reader.EndOfStream)
                return _endState;

            return ReadChar(reader.Read(), ref buffer, ref bufferPosition);
        }

        public virtual bool IsEndState { get { return false; } }
        public abstract StateReader ReadChar(int c, ref char[] buffer, ref int bufferPosition);

        public bool ContinueReadingLine
        {
            get { return _continueReadingLine; }
        }
        protected void StopReadingLine()
        {
            _continueReadingLine = false;
        }
    }
}

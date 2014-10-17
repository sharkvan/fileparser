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
    internal class EndState : StateReader
    {
        public override bool IsEndState
        {
            get
            {
                return true;
            }
        }

        public override StateReader ReadChar(int c, ref char[] buffer, ref int bufferPosition)
        {
            return this;
        }
    }
}

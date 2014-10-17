/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using FileParser.CharStateReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalBudget.Model.UnitTests
{
    [TestClass]
    public class EndNewLinePatternStateUnitTests
    {
        [TestMethod]
        public void PositiveReadTest()
        {
            char[] buffer = new char[1];
            int bufferPosition = 0;
            InitialState returnToState = new InitialState(',', '"');
            EndNewLinePatternState target = new EndNewLinePatternState('\n', returnToState);

            StateReader actualNextState = target.ReadChar((int)'\n', ref buffer, ref bufferPosition);

            Assert.ReferenceEquals(target, actualNextState);
        }

        [TestMethod]
        public void NegativeReadTest()
        {
            char[] buffer = new char[1];
            int bufferPosition = 0;
            InitialState returnToState = new InitialState(',', '"');
            EndNewLinePatternState target = new EndNewLinePatternState('\n', returnToState);

            StateReader actualNextState = target.ReadChar((int)'a', ref buffer, ref bufferPosition);

            Assert.ReferenceEquals(returnToState, actualNextState);
            Assert.AreEqual('a', buffer[0]);
        }
    }
}

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
    public class NewLinePatternStateUnitTests
    {
        [TestMethod]
        public void ReadFullNewLinePatternTest()
        {
            InitialState returnToState = new InitialState(',', '"');
            NewLinePatternState target = new NewLinePatternState(returnToState);
            char[] buffer = new char[1];
            int bufferPosition = 0;

            StateReader actualNextState = target.ReadChar((int)'\r', ref buffer, ref bufferPosition);
            Assert.AreNotEqual('\r', buffer[0]);
            Assert.IsTrue(actualNextState.GetType() == typeof(EndNewLinePatternState));

            actualNextState = actualNextState.ReadChar((int)'\n', ref buffer, ref bufferPosition);
            Assert.AreNotEqual('\r', buffer[0]);
            Assert.IsTrue(actualNextState.IsEndState);
            Assert.IsFalse(actualNextState.ContinueReadingLine);
        }

        [TestMethod]
        public void ReadNewLinePatternTest()
        {
            InitialState returnToState = new InitialState(',', '"');
            NewLinePatternState target = new NewLinePatternState(returnToState);
            char[] buffer = new char[1];
            int bufferPosition = 0;

            StateReader actualNextState = target.ReadChar((int)'\n', ref buffer, ref bufferPosition);
            Assert.AreNotEqual('\n', buffer[0]);
            Assert.IsTrue(actualNextState.IsEndState);
            Assert.IsFalse(actualNextState.ContinueReadingLine);
        }
    }
}

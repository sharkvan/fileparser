/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileParser.CharStateReader;

namespace PersonalBudget.Model.UnitTests
{
    [TestClass]
    public class DelimiterStateUnitTests
    {
        [TestMethod]
        public void ReadDelimiterCharTest()
        {
            char[] buffer = new char[1];
            int bufferPosition = 0;
            DelimiterState target = new DelimiterState(',', new EndState());

            StateReader resultState = target.ReadChar(',', ref buffer, ref bufferPosition);

            Assert.AreNotEqual(',', buffer[0]);
            Assert.IsTrue(resultState is EndState);
        }

        [TestMethod]
        public void ReadNonDelimiterCharTest()
        {
            char[] buffer = new char[1];
            int bufferPosition = 0;
            DelimiterState target = new DelimiterState(',', new EndState());

            StateReader resultState = target.ReadChar('a', ref buffer, ref bufferPosition);

            Assert.AreEqual('a', buffer[0]);
            Assert.AreEqual(1, bufferPosition);
            Assert.IsTrue(resultState is DelimiterState);
        }

        [TestMethod]
        public void ReadStartOfNewLineCharTest()
        {
            char[] buffer = new char[1];
            int bufferPosition = 0;
            DelimiterState target = new DelimiterState(',', new EndState());

            StateReader resultState = target.ReadChar('\r', ref buffer, ref bufferPosition);

            Assert.AreNotEqual('\r', buffer[0]);
            Assert.AreEqual(0, bufferPosition);
            Assert.IsTrue(resultState is EndNewLinePatternState);
        }

        [TestMethod]
        public void ReadEndOfNewLineCharTest()
        {
            char[] buffer = new char[2];
            int bufferPosition = 0;
            StateReader target = new DelimiterState(',', new EndState());

            target = target.ReadChar('\r', ref buffer, ref bufferPosition);

            Assert.AreNotEqual('\r', buffer[0]);
            Assert.AreEqual(0, bufferPosition);
            Assert.IsTrue(target is EndNewLinePatternState);

            target = target.ReadChar('\n', ref buffer, ref bufferPosition);

            Assert.AreNotEqual('\n', buffer[0]);
            Assert.AreEqual(0, bufferPosition);
            Assert.IsTrue(target is EndNewLinePatternState);
        }
    }
}

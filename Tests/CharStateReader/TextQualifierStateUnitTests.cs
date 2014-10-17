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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileParser.CharStateReader;

namespace PersonalBudget.Model.UnitTests
{
    [TestClass]
    public class TextQualifierStateUnitTests
    {
        [TestMethod]
        public void ReadCharQualifiedTextTest()
        {
            StartTextQualifierState target = 
                new StartTextQualifierState('"', 
                    new DelimiterState(',', new EndState()));
            char[] buffer = new char[1];
            int bufferPosition = 0;

            StateReader secondState = target.ReadChar((int)'"', ref buffer, ref bufferPosition);
            Assert.IsFalse(secondState.IsEndState);
            Assert.IsTrue(secondState.ContinueReadingLine);
            Assert.AreEqual(char.MinValue, buffer[0]);

            StateReader laststate = secondState.ReadChar((int)'"', ref buffer, ref bufferPosition);
            Assert.IsFalse(laststate.IsEndState);
            Assert.IsTrue(laststate.ContinueReadingLine);
            Assert.AreEqual(char.MinValue, buffer[0]);
        }

        [TestMethod]
        public void ReadQualifiedTextTest()
        {
            string testString = "\"Some qualified, text\"";
            MemoryStream source = new MemoryStream();
            StateReader target =
                new StartTextQualifierState('"',
                    new DelimiterState(',', new EndState()));

            StreamWriter writer = new StreamWriter(source);
            writer.Write(testString);
            writer.Flush();
            source.Seek(0, SeekOrigin.Begin);
            char[] buffer = new char[testString.Length];
            int bufferPosition = 0;

            StreamReader reader = new StreamReader(source);

            while (!target.IsEndState)
            {
                target = target.Read(reader, ref buffer, ref bufferPosition);
            }

            Assert.AreEqual(testString.Replace("\"", string.Empty), new string(buffer, 0, bufferPosition));
        }
    }
}
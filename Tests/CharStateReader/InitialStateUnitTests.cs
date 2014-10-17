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
    public class InitialStateUnitTests
    {
        [TestMethod]
        public void ReadDelimiterTest()
        {
            char[] buffer = new char[1];
            int bufferPosition = 0;
            StateReader target = new InitialState(',', '"');

            target = target.ReadChar(',', ref buffer, ref bufferPosition);

            Assert.AreNotEqual(',', buffer[0]);
            Assert.AreEqual(0, bufferPosition);
            Assert.IsTrue(target is EndState);
        }

        [TestMethod]
        public void ReadNonDelimiterTest()
        {
            char[] buffer = new char[1];
            int bufferPosition = 0;
            StateReader target = new InitialState(',', '"');

            target = target.ReadChar('a', ref buffer, ref bufferPosition);

            Assert.AreEqual('a', buffer[0]);
            Assert.AreEqual(1, bufferPosition);
            Assert.IsTrue(target is DelimiterState);
        }

        [TestMethod]
        public void ReadQualifiedTextTest()
        {
            char[] buffer = new char[1];
            int bufferPosition = 0;
            StateReader target = new InitialState(',', '"');

            target = target.ReadChar('"', ref buffer, ref bufferPosition);

            Assert.AreNotEqual('"', buffer[0]);
            Assert.AreEqual(0, bufferPosition);
            Assert.IsTrue(target is EndTextQualifierState);
        }

        [TestMethod]
        public void ReadQualifiedTextStringTest()
        {
            string testString = "\"Some qualified text\"";
            char[] buffer = new char[testString.Length];
            int bufferPosition = 0;
            StateReader target = new InitialState(',', '"');
            MemoryStream source = new MemoryStream();
            StreamWriter writer = new StreamWriter(source);
            writer.Write(testString);
            writer.Flush();
            source.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(source);
            
            while(!target.IsEndState)
                target = target.Read(reader, ref buffer, ref bufferPosition);

            Assert.AreEqual("Some qualified text", new string(buffer, 0, bufferPosition));
        }

        [TestMethod]
        public void ReadComboTextStringTest1()
        {
            string testString = "\"Some,qualified text\",deliminited text";
            char[] buffer = new char[testString.Length];
            int bufferPosition = 0;
            StateReader target = new InitialState(',', '"');
            MemoryStream source = new MemoryStream();
            StreamWriter writer = new StreamWriter(source);
            writer.Write(testString);
            writer.Flush();
            source.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(source);

            while (!target.IsEndState)
                target = target.Read(reader, ref buffer, ref bufferPosition);

            Assert.AreEqual("Some,qualified text", new string(buffer, 0, bufferPosition));
        }

        [TestMethod]
        public void ReadComboTextStringTest2()
        {
            string testString = "deliminited text,\"Some,qualified text\"";
            char[] buffer = new char[testString.Length];
            int bufferPosition = 0;
            StateReader target = new InitialState(',', '"');
            MemoryStream source = new MemoryStream();
            StreamWriter writer = new StreamWriter(source);
            writer.Write(testString);
            writer.Flush();
            source.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(source);

            while (!target.IsEndState)
                target = target.Read(reader, ref buffer, ref bufferPosition);

            Assert.AreEqual("deliminited text", new string(buffer, 0, bufferPosition));
        }
    }
}
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
using FileParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PersonalBudget.Model.Parser
{
    [TestClass]
    public class LineParserEnumeratorUnitTests
    {
        static ParserSettings settings = new ParserSettings();

        [TestMethod]
        public void ReadQualifiedFirstDelimitedSecondMultiLineTest()
        {
            string testString = "\"Some,qualified text\",deliminited text" + Environment.NewLine + "\"Some,qualified text\",deliminited text";
            MemoryStream source = new MemoryStream();
            StreamWriter writer = new StreamWriter(source);
            writer.Write(testString);
            writer.Flush();
            source.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(source);

            List<string> results = new List<string>();

            foreach (IEnumerable<string> item in
                (new LineEnumerable(reader, settings)))
            {
                foreach (string str in item)
                {
                    results.Add(str);
                }
            }

            Assert.AreEqual(4, results.Count);
            Assert.AreEqual("Some,qualified text", results[0]);
            Assert.AreEqual("deliminited text", results[1]);
            Assert.AreEqual("Some,qualified text", results[2]);
            Assert.AreEqual("deliminited text", results[3]);
        }
    }
}

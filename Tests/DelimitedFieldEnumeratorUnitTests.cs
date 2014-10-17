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

namespace PersonalBudget.Model.UnitTests
{
    [TestClass]
    public class DelimitedFieldParserEnumeratorUnitTests
    {
        static ParserSettings settings = new ParserSettings();

        [TestMethod]
        public void ReadDelimitedFirstQualifiedSecondTest()
        {
            string testString = "deliminited text,\"Some,qualified text\"";
            MemoryStream source = new MemoryStream();
            StreamWriter writer = new StreamWriter(source);
            writer.Write(testString);
            writer.Flush();
            source.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(source);

            List<string> results = new List<string>(
                new FieldEnumerable(reader, settings));

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("deliminited text", results[0]);
            Assert.AreEqual("Some,qualified text", results[1]);
        }

        [TestMethod]
        public void ReadQualifiedFirstDelimitedSecondTest()
        {
            string testString = "\"Some,qualified text\",deliminited text";
            MemoryStream source = new MemoryStream();
            StreamWriter writer = new StreamWriter(source);
            writer.Write(testString);
            writer.Flush();
            source.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(source);

            List<string> results = new List<string>(
                new FieldEnumerable(reader, settings));

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("Some,qualified text", results[0]);
            Assert.AreEqual("deliminited text", results[1]);
        }

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

            List<string> results = new List<string>(
                new FieldEnumerable(reader, settings));

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("Some,qualified text", results[0]);
            Assert.AreEqual("deliminited text", results[1]);
        }

        [TestMethod]
        public void ReadDelimitedFirstQualifiedSecondMultiLineTest()
        {
            string testString = "deliminited text,\"Some,qualified text\"" + Environment.NewLine + "\"Some,qualified text\",deliminited text";
            MemoryStream source = new MemoryStream();
            StreamWriter writer = new StreamWriter(source);
            writer.Write(testString);
            writer.Flush();
            source.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(source);

            List<string> results = new List<string>(
                new FieldEnumerable(reader, settings));

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("deliminited text", results[0]);
            Assert.AreEqual("Some,qualified text", results[1]);
        }

        [TestMethod]
        public void FieldExceedsBuffer_ArrayAutoExpands()
        {
            string longField = "really long field of text lkajfoiclakcnalskjdlaksdmalksjdlakjdalcjalksdjalksdjalsjdaisjdaklsdjlaksjdlakjdlaksjdlakdjlaksdjlaksjdalkjsdalkjdalksjdalkdjalskjdlaskjdlaksjdlaskjdalskjdlaksdjlaksjdlaskjdalskjdlaskjdlaksdjlaskdjalskdjalskjdlaksjdlaskjdlaskjdlaskjdaskjdlajdlaksclaksjdlaisjdlaksdjlajawijdaowjdaksjdlakjsdlaijsdlaksjdalksjdlaksjdalksdjalskdjalskdjalksdjlaksdjalskdjalskdjalskdjalksdjasijclaksdjlaskjdaijwkadlksjdlasjdaowijdaksljdlaksjdoaisjdclaksdmlawkdjlasijclaksdlakwdjlaksjdaisjcalksdjlakjdpasodjapwokdaskjdlkefjnoiejflskdjfwleknflskdjclsjdhflkjvnsjdhfsIUdchLASKJdfncASKhjfcOSAKJcnKASJNcfSHFJLAKSJCSALKjdfSidjfkljzkdgjdskfjgjkdhfglsdkfjghlskdjfhalskdjhfalsdkjfhalskdjfhdfnzdkfjgdkfngzdkvfnzdkfjgkljsdfhalskdfhjalsdkfjhalskdfjhalskfhalskdfjhalskdjfhalksdjfhalskdfjhalskdjfhalskdjfhalsdkfjhalsdkjfhalsdkjfhalsdkjfhalsdkjhfalskdjfhalskdjfhlasdkjhfalskdjhfalsdkjfhalskdjfhlasdkjfhlasdkjfhalskdjfhalsdkjfhalsdkjhflasdkjfhlasdkjfhalsdkjfhlaskdjhfalskdjfhalskdjfhasdjfhlasdkfhjalsdkfjhalsdkfkjdshfksjdfhskdjfhskdjfhskdjfhksdjfhksdjhfskdjhfksdjhfksdjfhskdjfhksdjfhksdjhfksdjfhksdjfhskdjfhksdjfhksdjfhksjdfhksdjhf";
            string testString = "deliminited text";
            MemoryStream source = new MemoryStream();
            StreamWriter writer = new StreamWriter(source);
            writer.Write(string.Format("{0},{1}{2}{1},{0}", testString, longField, Environment.NewLine));
            writer.Flush();
            source.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(source);

            List<string> results = new List<string>(
                new FieldEnumerable(reader, settings));

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(testString, results[0]);
            Assert.AreEqual(longField, results[1]);

        }
    }
}

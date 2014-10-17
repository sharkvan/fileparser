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

namespace PersonalBudget.Model.UnitTests
{
    [TestClass]
    public class ParserSettingsUnitTests
    {
        [TestMethod]
        public void DefaultContructorTest()
        {
            ParserSettings target = new ParserSettings();
            Assert.AreEqual(LayoutType.Delimited, target.Layout);
            Assert.AreEqual(',', target.FieldDelimiter);
            Assert.AreEqual('"', target.TextQualifier);
        }
    }
}

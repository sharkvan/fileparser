using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FileParser.Tests
{
    [TestClass]
    public class ParserBuilderTests
    {
        [TestMethod]
        public void GivenAFilePathWhenCallingCreateThenReceiveAnEnumerable()
        {
            int expecedRecordsCount = 800001; //One less than the source file because we are zero based.
            int actualRecordCount = 0;

            using (var parser = ParserBuilder.Create(".\\MOCK_DATA.csv"))
            {
                foreach (var item in parser)
                {
                    ++actualRecordCount;
                    foreach (var field in item) ;
                }
            }

            Assert.AreEqual(expecedRecordsCount, actualRecordCount);
        }

        [TestMethod]
        public void GivenAFilePath()
        {
            int expecedRecordsCount = 800001; //One less than the source file because we are zero based.
            int actualRecordCount = 0;
            using (var parser = ParserBuilder.Create(".\\MOCK_DATA.csv", ParserSettings.TSV))
            {
                foreach (var item in parser)
                {
                    ++actualRecordCount;
                    foreach (var field in item) ;
                }
            }
            
            Assert.AreEqual(expecedRecordsCount, actualRecordCount);
        }

    }
}

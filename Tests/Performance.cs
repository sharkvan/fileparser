/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FileParser.Tests
{
    [TestClass]
    public class Performance
    {
        [TestMethod]
        public void ReadALargerFile()
        {
            using  (FileStream stream = new FileStream(".\\MOCK_DATA.csv", FileMode.Open, FileAccess.Read, FileShare.None, 4096))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    int expecedRecordsCount = 800001; //One less than the source file because we are zero based.
                    int actualRecordCount = 0;

                    foreach (var item in new LineEnumerable(reader, new ParserSettings(',', '"')))
                    {
                        ++actualRecordCount;
                        using(IEnumerator<string> fields = item.GetEnumerator())
                        {
                            while (fields.MoveNext()) ;
                        }
                    }

                    Assert.AreEqual(expecedRecordsCount, actualRecordCount);
                }
            }
        }
    }
}

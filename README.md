# Welcome

FileParser is a stream based utility for reading delimited text; such as CSV, with an IEnumerable<string> interface. The read can also handle files of fixed width files. It can read any size file as it only buffers a small amount of the file in memory.

##Files you can read
* Delimited text - the delimiter is currently limited to one character
* Supports quoted strings - the character for qualifying quoted text is configurable. This allows the configured delimiter to be in the quoted text.
* There is no real limit to how many columns or rows can be read. This is because only one field for one row is read at a time. Allowing for a very low memory foot print and still very fast.

##Examples##

```
#!c#

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
```


### Contribution guidelines ###

* Unit tests are required for each contribution

### Road Map ###
* API Increase usability
* Create performance benchmark comparisons
* Allow for column types to be defined
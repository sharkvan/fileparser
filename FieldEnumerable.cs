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

namespace FileParser
{
    internal class FieldEnumerable : IEnumerable<string>
    {
        readonly StreamReader _reader;
        readonly ParserSettings _settings;

        public FieldEnumerable(StreamReader reader, ParserSettings settings)
        {
            _reader = reader;
            _settings = settings;
        }

        public IEnumerator<string> GetEnumerator()
        {
            switch (_settings.Layout)
            {
                case LayoutType.Delimited:
                    return new DelimitedFieldEnumerator(_reader, _settings);
                case LayoutType.FixedWidth:
                    return new FixedWidthFieldEnumerator(_reader, _settings);
                default:
                    throw new ApplicationException("Unknown file layout type");
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileParser
{
    public class ParserSettings
    {
        private readonly LayoutType _layout;
        private readonly char _fieldDelimiter;
        private readonly char _textQualifier;
        private readonly IEnumerable<int> _fieldSizes;

        public ParserSettings() : this (',', '"') { }

        public ParserSettings(char fieldDelimiter, char textQualifier)
        {
            _layout = LayoutType.Delimited;
            _fieldDelimiter = fieldDelimiter;
            _textQualifier = textQualifier;
        }

        public ParserSettings(IEnumerable<int> fieldSizes)
        {
            _layout = LayoutType.FixedWidth;
            _fieldSizes = fieldSizes;
        }

        public LayoutType Layout
        {
            get { return _layout; }
        }

        public char FieldDelimiter
        {
            get { return _fieldDelimiter; }
        }

        public char TextQualifier
        {
            get { return _textQualifier; }
        }

        public IEnumerable<int> FieldSizes
        {
            get { return _fieldSizes; }
        }
    }
}
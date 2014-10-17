/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser.PatternsAndAlgorithms
{
    internal class Pattern
    {
        internal static void DisposeClass<TYPE>(ref TYPE _fieldSizes)
            where TYPE : class, IDisposable
        {
            if(_fieldSizes != null)
            {
                _fieldSizes.Dispose();
                _fieldSizes = null;
            }
        }
    }
}

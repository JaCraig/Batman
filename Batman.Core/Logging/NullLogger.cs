﻿/*
Copyright (c) 2013 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using Batman.Core.Logging.BaseClasses;
using Utilities.IO.Logging.Enums;

namespace Batman.Core.Logging
{
    /// <summary>
    /// Null logger (if one isn't found)
    /// </summary>
    public class NullLogger : LogBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullLogger"/> class.
        /// </summary>
        public NullLogger()
            : base(x => { })
        {
            End = x => { };
            Log.Add(MessageType.Debug, x => { });
            Log.Add(MessageType.Error, x => { });
            Log.Add(MessageType.General, x => { });
            Log.Add(MessageType.Info, x => { });
            Log.Add(MessageType.Trace, x => { });
            Log.Add(MessageType.Warn, x => { });
            FormatMessage = (Message, Type, args) => Message;
        }
    }
}
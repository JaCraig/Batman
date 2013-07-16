/*
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

#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Batman.Core.Bootstrapper.Interfaces;

using Utilities.DataTypes.ExtensionMethods;
using Batman.Core.Logging.BaseClasses;
using Utilities.IO.Logging.Enums;
using Batman.Core.Logging;
using System.IO;
using Batman.Core.Tasks;
using Batman.Core.Tasks.Enums;
using Batman.Core.FileSystem;
using Batman.Core.Communication;
#endregion

namespace Batman.Core.Profiling.Interfaces
{
    /// <summary>
    /// Profiler interface
    /// </summary>
    public interface IProfiler : IDisposable
    {
        /// <summary>
        /// Name of the profiler
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Creates a new profiler that is a sub section of the current profiler
        /// </summary>
        /// <returns>A new profiler</returns>
        IProfiler Step(string Name="");

        /// <summary>
        /// Starts the profiler
        /// </summary>
        /// <returns>this</returns>
        IProfiler Start();

        /// <summary>
        /// Stops the profiler
        /// </summary>
        /// <param name="DiscardResults">Should the results be discarded?</param>
        /// <returns>this</returns>
        IProfiler Stop(bool DiscardResults);
    }
}
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

using Batman.Core.Profiling.BaseClasses;
using StackExchange.Profiling;
using System;

namespace Batman.Core.Profiling.MiniProfiler
{
    /// <summary>
    /// MiniProfiler based profiler
    /// </summary>
    public class Profile : ProfilerBase
    {
        public Profile()
        {
            this.Current = StackExchange.Profiling.MiniProfiler.Current;
        }

        public Profile(StackExchange.Profiling.MiniProfiler ProfilerUsing, IDisposable StepDisposable)
        {
            this.Current = ProfilerUsing ?? StackExchange.Profiling.MiniProfiler.Current;
            this.StepDisposable = StepDisposable;
        }

        public override string Name { get { return "MiniProfiler"; } }
        private StackExchange.Profiling.MiniProfiler Current { get; set; }

        private IDisposable StepDisposable { get; set; }

        public override void Dispose(bool Managed)
        {
            if (StepDisposable != null)
            {
                StepDisposable.Dispose();
            }
        }

        public override Interfaces.IProfiler Start()
        {
            StackExchange.Profiling.MiniProfiler.Start();
            return this;
        }

        public override Interfaces.IProfiler Step(string Name)
        {
            return new Profile(Current, Current.Step(Name));
        }

        public override Interfaces.IProfiler Stop(bool DiscardResults)
        {
            StackExchange.Profiling.MiniProfiler.Stop(DiscardResults);
            return this;
        }
    }
}
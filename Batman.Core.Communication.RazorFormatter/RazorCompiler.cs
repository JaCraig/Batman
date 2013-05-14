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
using Batman.Core.Bootstrapper.Interfaces;
using System.Collections.Generic;
using Batman.Core.FileSystem;
using System.Linq;
using System.IO;
using Batman.Core.FileSystem.Interfaces;
using System.Web;
using Batman.Core.Tasks.Interfaces;
using Batman.Core;
using System.Dynamic;
#endregion

namespace Batman.Core.Communication.RazorFormatter
{
    /// <summary>
    /// Razor compiler task
    /// </summary>
    public class RazorCompiler : ITask
    {
        public Core.Tasks.Enums.RunTime TimeToRun
        {
            get { return Batman.Core.Tasks.Enums.RunTime.PostStart; }
        }

        public string Name
        {
            get { return "Razor template compiler"; }
        }

        public void Run()
        {
            IDirectory Directory = BatComputer.Bootstrapper.Resolve<FileManager>().Directory("~/Templates/");
            if (!Directory.Exists)
                return;
            foreach (IFile File in Directory.EnumerateFiles(new string[] { "*.cshtml" }, SearchOption.AllDirectories))
            {
                RazorEngine.Razor.Compile(File.Read(), File.FullName);
            }
        }
    }
}
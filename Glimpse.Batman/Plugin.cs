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
using System.Text;
using Glimpse.Core.Extensibility;
using System.Collections.Generic;
using Batman.Core;
using Batman.Core.FileSystem;
using Batman.Core.Communication;
using Batman.Core.Profiling.Interfaces;
using Batman.Core.Tasks;
using Batman.MVC.Assets;
using Batman.Core.Serialization;
using Glimpse.AspNet.Extensibility;
#endregion

namespace Glimpse.Batman
{
    /// <summary>
    /// Plugin that displays info from Batman
    /// </summary>
    public class Plugin : AspNetTab, IDocumentation
    {
        public string DocumentationUri { get { return "http://www.gutgames.com"; } }

        public override string Name { get { return "Batman"; } }

        public override object GetData(ITabContext context)
        {
            Dictionary<string, string[]> Return = new Dictionary<string, string[]>();
            Return.Add("Bootstrapper", new string[] { BatComputer.Bootstrapper.Name });
            Return.Add("File systems", BatComputer.Bootstrapper.Resolve<FileManager>().ToString().Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries));
            Return.Add("Communication systems", BatComputer.Bootstrapper.Resolve<CommunicationManager>().ToString().Split(new string[]{"\n"}, StringSplitOptions.RemoveEmptyEntries));
            Return.Add("Profiler", BatComputer.Bootstrapper.Resolve<IProfiler>().ToString().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
            Return.Add("Tasks", BatComputer.Bootstrapper.Resolve<TaskManager>().ToString().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
            Return.Add("Asset Management", BatComputer.Bootstrapper.Resolve<AssetManager>().ToString().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
            Return.Add("Serializers", BatComputer.Bootstrapper.Resolve<SerializationManager>().ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            return Return;
        }
    }
}

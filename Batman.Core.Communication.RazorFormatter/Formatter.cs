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
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Dynamic;
using Batman.Core.Communication.Interfaces;
using RazorEngine.Templating;
using Batman.Core.FileSystem;
using System.Linq;
using System.IO;
#endregion

namespace Batman.Core.Communication.SMTP
{
    /// <summary>
    /// Razor based formatter
    /// </summary>
    public class Formatter : IFormatter
    {
        public Formatter() { }

        public string Name { get { return "Razor"; } }

        public IMessage Format<T>(IMessage Message, string Template, T Model)
        {
            return Format(Message,
                          BatComputer.Bootstrapper.Resolve<FileManager>()
                                                  .Directory("~/Templates/")
                                                  .EnumerateFiles(new string[] { Template + ".cshtml" }, SearchOption.AllDirectories)
                                                  .FirstOrDefault(),
                          Model);
        }

        public IMessage Format<T>(IMessage Message, FileSystem.Interfaces.IFile Template, T Model)
        {
            if (Template == null || !Template.Exists)
                return Message;
            Message.Body = RazorEngine.Razor.Run(Template.FullName, Model);
            return Message;
        }
    }
}
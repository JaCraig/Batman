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
using Batman.Core.FileSystem.Interfaces;
using System.Web;
using System.IO;
using Utilities.IO.ExtensionMethods;
#endregion

namespace Batman.Core.FileSystem.Local
{
    /// <summary>
    /// Basic file class
    /// </summary>
    public class File : IFile
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Path">Path to the file</param>
        public File(string Path)
        {
            InternalFile = new FileInfo(Path);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="File">File to use</param>
        public File(FileInfo File)
        {
            this.InternalFile = File;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Internal pointer to the file system
        /// </summary>
        protected FileInfo InternalFile { get; private set; }

        public DateTime Accessed
        {
            get { return InternalFile.LastAccessTimeUtc; }
        }

        public DateTime Created
        {
            get { return InternalFile.CreationTimeUtc; }
        }

        public DateTime Modified
        {
            get { return InternalFile.LastWriteTimeUtc; }
        }

        public IDirectory Directory
        {
            get { return new Directory(InternalFile.Directory); }
        }

        public bool Exists
        {
            get { return InternalFile.Exists; }
        }

        public string Extension
        {
            get { return InternalFile.Extension; }
        }

        public string FullName
        {
            get { return InternalFile.FullName; }
        }

        public long Length
        {
            get { return InternalFile.Exists ? InternalFile.Length : 0; }
        }

        public string Name
        {
            get { return InternalFile.Name; }
        }

        #endregion

        #region Functions

        public void Delete()
        {
            if (!Exists)
                return;
            InternalFile.Delete();
            InternalFile.Refresh();
        }

        public string Read()
        {
            return InternalFile.Read();
        }

        public byte[] ReadBinary()
        {
            return InternalFile.ReadBinary();
        }

        public void Rename(string NewName)
        {
            InternalFile.MoveTo(InternalFile.DirectoryName + "\\" + NewName);
            InternalFile = new FileInfo(InternalFile.DirectoryName + "\\" + NewName);
        }

        public void MoveTo(IDirectory Directory)
        {
            InternalFile.MoveTo(Directory.FullName + "\\" + Name);
            InternalFile = new FileInfo(Directory.FullName + "\\" + Name);
        }

        public void Write(string Content, System.IO.FileMode Mode = FileMode.Create, Encoding Encoding = null)
        {
            InternalFile.Save(Content, Mode, Encoding);
            InternalFile.Refresh();
        }

        public void Write(byte[] Content, System.IO.FileMode Mode = FileMode.Create)
        {
            InternalFile.Save(Content, Mode);
            InternalFile.Refresh();
        }

        #endregion
    }
}
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
    /// Directory class
    /// </summary>
    public class Directory : IDirectory
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Path">Path to the directory</param>
        public Directory(string Path)
        {
            this.InternalDirectory = new DirectoryInfo(Path);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Directory">Internal directory</param>
        public Directory(DirectoryInfo Directory)
        {
            this.InternalDirectory = Directory;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Internal directory
        /// </summary>
        protected DirectoryInfo InternalDirectory { get; private set; }

        public DateTime Accessed
        {
            get { return InternalDirectory.LastAccessTimeUtc; }
        }

        public DateTime Created
        {
            get { return InternalDirectory.CreationTimeUtc; }
        }

        public DateTime Modified
        {
            get { return InternalDirectory.LastWriteTimeUtc; }
        }

        public bool Exists
        {
            get { return InternalDirectory.Exists; }
        }

        public string FullName
        {
            get { return InternalDirectory.FullName; }
        }

        public string Name
        {
            get { return InternalDirectory.Name; }
        }

        public IDirectory Parent
        {
            get { return new Directory(InternalDirectory.Parent); }
        }

        public IDirectory Root
        {
            get { return new Directory(InternalDirectory.Root); }
        }

        public long Size
        {
            get { return Exists ? InternalDirectory.Size("*", true) : 0; }
        }

        #endregion

        #region Functions

        public void Create()
        {
            InternalDirectory.Create();
            InternalDirectory.Refresh();
        }

        public void Delete()
        {
            if (!Exists)
                return;
            InternalDirectory.DeleteAll();
            InternalDirectory.Refresh();
        }

        public IEnumerable<IDirectory> EnumerateDirectories(string SearchPattern, SearchOption Options = SearchOption.TopDirectoryOnly)
        {
            foreach (DirectoryInfo SubDirectory in InternalDirectory.EnumerateDirectories(SearchPattern, Options))
            {
                yield return new Directory(SubDirectory);
            }
        }

        public IEnumerable<IFile> EnumerateFiles(IEnumerable<string> SearchPattern, SearchOption Options = SearchOption.TopDirectoryOnly)
        {
            foreach (FileInfo File in InternalDirectory.EnumerateFiles(SearchPattern, Options))
            {
                yield return new File(File);
            }
        }

        public void MoveTo(IDirectory Directory)
        {
            InternalDirectory.MoveTo(Directory.FullName + "\\" + Name);
            InternalDirectory = new DirectoryInfo(Directory.FullName + "\\" + Name);
        }

        public void Rename(string Name)
        {
            InternalDirectory.MoveTo(Parent.FullName + "\\" + Name);
            InternalDirectory = new DirectoryInfo(Parent.FullName + "\\" + Name);
        }

        public override string ToString()
        {
            return FullName;
        }

        #endregion
    }
}
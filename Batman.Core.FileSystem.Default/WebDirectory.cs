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

using Batman.Core.FileSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities.DataTypes.ExtensionMethods;

namespace Batman.Core.FileSystem.Local
{
    /// <summary>
    /// Directory class
    /// </summary>
    public class WebDirectory : IDirectory
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Path">Path to the directory</param>
        public WebDirectory(string Path)
        {
            this.InternalDirectory = new Uri(Path);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Directory">Internal directory</param>
        public WebDirectory(Uri Directory)
        {
            this.InternalDirectory = Directory;
        }

        public DateTime Accessed
        {
            get { return DateTime.Now; }
        }

        public DateTime Created
        {
            get { return DateTime.Now; }
        }

        public bool Exists
        {
            get { return true; }
        }

        public string FullName
        {
            get { return InternalDirectory.AbsolutePath; }
        }

        public DateTime Modified
        {
            get { return DateTime.Now; }
        }

        public string Name
        {
            get { return InternalDirectory.AbsolutePath; }
        }

        public IDirectory Parent
        {
            get { return new WebDirectory(InternalDirectory.AbsolutePath.Left(InternalDirectory.AbsolutePath.LastIndexOf("/", StringComparison.Ordinal) - 1)); }
        }

        public IDirectory Root
        {
            get { return new Directory(InternalDirectory.Scheme + "://" + InternalDirectory.Host); }
        }

        public long Size
        {
            get { return 0; }
        }

        /// <summary>
        /// Internal directory
        /// </summary>
        protected Uri InternalDirectory { get; private set; }

        public void Create()
        {
        }

        public void Delete()
        {
        }

        public IEnumerable<IDirectory> EnumerateDirectories(string SearchPattern, SearchOption Options = SearchOption.TopDirectoryOnly)
        {
            return new List<WebDirectory>();
        }

        public IEnumerable<IFile> EnumerateFiles(IEnumerable<string> SearchPattern, SearchOption Options = SearchOption.TopDirectoryOnly)
        {
            return new List<WebFile>();
        }

        public void MoveTo(IDirectory Directory)
        {
        }

        public void Rename(string Name)
        {
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
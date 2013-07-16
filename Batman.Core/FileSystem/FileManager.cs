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
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
#endregion

namespace Batman.Core.FileSystem
{
    /// <summary>
    /// File system manager
    /// </summary>
    public class FileManager
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FileManager()
        {
            FileSystems = new Dictionary<string, IFileSystem>();
            IEnumerable<Assembly> Assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (IFileSystem FileSystem in Assemblies.Objects<IFileSystem>())
            {
                FileSystems.Add(FileSystem.Name, FileSystem);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// File systems
        /// </summary>
        protected IDictionary<string, IFileSystem> FileSystems { get; private set; }

        /// <summary>
        /// Gets the file system by name
        /// </summary>
        /// <param name="Name">Name of the file system</param>
        /// <returns>The file system specified</returns>
        public IFileSystem this[string Name] { get { return FileSystems.ContainsKey(Name) ? FileSystems[Name] : null; } }

        #endregion

        #region Functions

        /// <summary>
        /// Gets the class representation for the file
        /// </summary>
        /// <param name="Path">Path to the file</param>
        /// <returns>The file object</returns>
        public IFile File(string Path)
        {
            IFileSystem FileSystem = FindSystem(Path);
            return FileSystem == null ? null : FileSystem.File(Path);
        }

        /// <summary>
        /// Gets the directory representation for the directory
        /// </summary>
        /// <param name="Path">Path to the directory</param>
        /// <returns>The directory object</returns>
        public IDirectory Directory(string Path)
        {
            IFileSystem FileSystem = FindSystem(Path);
            return FileSystem == null ? null : FileSystem.Directory(Path);
        }

        /// <summary>
        /// Finds a file system compatible with the path
        /// </summary>
        /// <param name="Path">Path to search for</param>
        /// <returns>The file system associated with the path</returns>
        protected IFileSystem FindSystem(string Path)
        {
            foreach (string Key in FileSystems.Keys)
            {
                if (FileSystems[Key].CanHandle(Path))
                    return FileSystems[Key];
            }
            return null;
        }

        /// <summary>
        /// Outputs the file system information in string format
        /// </summary>
        /// <returns>The list of file systems that are available</returns>
        public override string ToString()
        {
            return FileSystems.Keys.ToString(x => x);
        }

        #endregion
    }
}
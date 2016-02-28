using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Batman.Core.FileSystem;
using Batman.Core.FileSystem.Interfaces;
using System.IO;

using Utilities.DataTypes.ExtensionMethods;

namespace Batman.Core.Tests.FileSystem
{
    public class LocalTest:IDisposable
    {
        public LocalTest() { new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).LoadAssemblies(false).ForEach(x => x); }
        [Fact]
        public void InitializationTest()
        {
            var Manager = new FileManager();
            Assert.NotEqual(null, Manager["Relative Local"]);
            Assert.NotEqual(null, Manager["Absolute Local"]);
            Assert.NotEqual(null, Manager["Network"]);
        }

        [Fact]
        public void DirectoryTest()
        {
            
            var Manager = new FileManager();
            IDirectory TempObject = Manager.Directory("~/Data/Test");
            Assert.Equal(false, TempObject.Exists);
            Assert.Equal("Test", TempObject.Name);
            Assert.Equal("Data", TempObject.Parent.Name);
            Assert.Equal(0, TempObject.Size);
            TempObject.Create();
            Assert.Equal(true, TempObject.Exists);
            Assert.Equal("Test", TempObject.Name);
            Assert.Equal("Data", TempObject.Parent.Name);
            TempObject.Delete();
            Assert.Equal(false, TempObject.Exists);
            Assert.Equal("Test", TempObject.Name);
            Assert.Equal("Data", TempObject.Parent.Name);
        }

        [Fact]
        public void DirectoryRenameTest()
        {
            
            var Manager = new FileManager();
            IDirectory TempObject = Manager.Directory("~/Data/Test");
            TempObject.Create();
            string DirectoryPath = TempObject.Parent.FullName;
            TempObject.Rename("Test2");
            Assert.Equal(DirectoryPath + "\\Test2", TempObject.FullName);
            TempObject.Delete();
        }

        [Fact]
        public void DirectoryMoveToTest()
        {
            
            var Manager = new FileManager();
            IDirectory TempObject = Manager.Directory("~/Data/Test");
            TempObject.Create();
            IDirectory NewParent = Manager.Directory("~/Data2/");
            NewParent.Create();
            TempObject.MoveTo(NewParent);
            Assert.Equal(NewParent.FullName + "Test", TempObject.FullName);
            TempObject.Delete();
        }

        [Fact]
        public void FileTest()
        {
            
            var Manager = new FileManager();
            IFile TempObject = Manager.File("~/Data/Test/Data.txt");
            Assert.Equal(false, TempObject.Exists);
            Assert.Equal("Data.txt", TempObject.Name);
            Assert.Equal("Test", TempObject.Directory.Name);
            Assert.Equal(0, TempObject.Length);
            TempObject.Write("This is a test");
            Assert.Equal(true, TempObject.Exists);
            Assert.Equal("Data.txt", TempObject.Name);
            Assert.Equal("Test", TempObject.Directory.Name);
            Assert.Equal(14, TempObject.Length);
            TempObject.Delete();
            Assert.Equal(false, TempObject.Exists);
            Assert.Equal("Data.txt", TempObject.Name);
            Assert.Equal("Test", TempObject.Directory.Name);
            Assert.Equal(0, TempObject.Length);
        }

        [Fact]
        public void FileRenameTest()
        {
            
            var Manager = new FileManager();
            IFile TempObject = Manager.File("~/Data/Test/Data.txt");
            TempObject.Write("This is a test");
            string DirectoryPath = TempObject.Directory.FullName;
            TempObject.Rename("Test2.txt");
            Assert.Equal(DirectoryPath + "\\Test2.txt", TempObject.FullName);
            TempObject.Delete();
        }

        [Fact]
        public void FileMoveToTest()
        {
            
            var Manager = new FileManager();
            IFile TempObject = Manager.File("~/Data/Test/Data.txt");
            TempObject.Write("This is a test");
            IDirectory NewParent = Manager.Directory("~/Data2/");
            NewParent.Create();
            TempObject.MoveTo(NewParent);
            Assert.Equal(NewParent.FullName + "Data.txt", TempObject.FullName);
            TempObject.Delete();
        }

        public void Dispose()
        {
            var Manager = new FileManager();
            Manager.Directory("~/Data/").Delete();
            Manager.Directory("~/Data2/").Delete();
        }
    }
}

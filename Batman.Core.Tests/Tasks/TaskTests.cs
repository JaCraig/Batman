using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Batman.Core.FileSystem;
using Batman.Core.FileSystem.Interfaces;
using Batman.Core.Tasks;
using Batman.Core.Tasks.Enums;
using Batman.Core.Tasks.Interfaces;
using Utilities.DataTypes.ExtensionMethods;
using Xunit;

namespace Batman.Core.Tests.Tasks
{
    public class SuperTask : ITask
    {
        public string Name
        {
            get { return "SuperTask"; }
        }

        public Core.Tasks.Enums.RunTime TimeToRun
        {
            get { return RunTime.PreStart; }
        }

        public void Run()
        {
        }
    }

    public class TaskTests
    {
        [Fact]
        public void BasicTest()
        {
            TaskManager Manager = new TaskManager();
            Assert.DoesNotThrow(() => Manager.Run(RunTime.PreStart));
            Assert.Equal(2, Manager.Tasks.Count);
        }
    }
}
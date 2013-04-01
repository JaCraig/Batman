using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Batman.Core.FileSystem;
using Batman.Core.FileSystem.Interfaces;
using System.IO;
using Utilities.Reflection.ExtensionMethods;
using Utilities.DataTypes.ExtensionMethods;
using Batman.Core.Tasks.Interfaces;
using Batman.Core.Tasks.Enums;
using Batman.Core.Tasks;

namespace Batman.Core.Tests.Tasks
{
    public class TaskTests
    {
        [Fact]
        public void BasicTest()
        {
            TaskManager Manager = new TaskManager();
            Assert.DoesNotThrow(() => Manager.Run(RunTime.PreStart));
            Assert.Equal(1, Manager.Tasks.Count);
        }
    }

    public class SuperTask : ITask
    {
        public Core.Tasks.Enums.RunTime TimeToRun
        {
            get { return RunTime.PreStart; }
        }

        public string Name
        {
            get { return "SuperTask"; }
        }

        public void Run()
        {
            
        }
    }
}

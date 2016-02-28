using Batman.Core.Tasks;
using Batman.Core.Tasks.Enums;
using Batman.Core.Tasks.Interfaces;
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
            var Manager = new TaskManager();
            Manager.Run(RunTime.PreStart);
            Assert.Equal(1, Manager.Tasks.Count);
        }
    }
}
using Xunit;

namespace Batman.Core.Tests
{
    public class BatComputerTests
    {
        [Fact]
        public void End()
        {
            BatComputer.End();
            Assert.Equal(null, BatComputer.Bootstrapper);
        }

        [Fact]
        public void Start()
        {
            BatComputer.PreStart();
            Assert.IsType<Batman.Core.Bootstrapper.TinyIoC.Bootstrapper>(BatComputer.Bootstrapper);
            BatComputer.PostStart();
        }
    }
}
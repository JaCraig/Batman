using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Batman.Core.Tests
{
    public class BatComputerTests
    {
        [Fact]
        public void Start()
        {
            Assert.DoesNotThrow(() => BatComputer.PreStart());
            Assert.IsType<Batman.Core.Bootstrapper.TinyIoC.Bootstrapper>(BatComputer.Bootstrapper);
            Assert.DoesNotThrow(() => BatComputer.PostStart());
        }

        [Fact]
        public void End()
        {
            BatComputer.End();
            Assert.Equal(null, BatComputer.Bootstrapper);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Batman.Core.Tests.Bootstrapper
{
    public class TinyIoCTest
    {
        [Fact]
        public void RegisterGenericTest()
        {
            using (Batman.Core.Bootstrapper.TinyIoC.Bootstrapper Bootstrapper = new Core.Bootstrapper.TinyIoC.Bootstrapper())
            {
                Assert.DoesNotThrow(() => Bootstrapper.Register<TestClass>());
                Assert.NotEqual(null, Bootstrapper.Resolve<TestClass>());
            }
        }

        [Fact]
        public void RegisterGenericNamedTest()
        {
            using (Batman.Core.Bootstrapper.TinyIoC.Bootstrapper Bootstrapper = new Core.Bootstrapper.TinyIoC.Bootstrapper())
            {
                Assert.DoesNotThrow(() => Bootstrapper.Register<TestClass>("Item1"));
                Assert.NotEqual(null, Bootstrapper.Resolve<TestClass>("Item1"));
            }
        }

        [Fact]
        public void RegisterGenericObjectTest()
        {
            using (Batman.Core.Bootstrapper.TinyIoC.Bootstrapper Bootstrapper = new Core.Bootstrapper.TinyIoC.Bootstrapper())
            {
                Assert.DoesNotThrow(() => Bootstrapper.Register<TestClass>(new TestClass() { A = 10, B = 1.23f, C = "Testing" }));
                Assert.Equal(new TestClass() { A = 10, B = 1.23f, C = "Testing" }, Bootstrapper.Resolve<TestClass>());
            }
        }

        [Fact]
        public void RegisterGenericObjectNamedTest()
        {
            using (Batman.Core.Bootstrapper.TinyIoC.Bootstrapper Bootstrapper = new Core.Bootstrapper.TinyIoC.Bootstrapper())
            {
                Assert.DoesNotThrow(() => Bootstrapper.Register<TestClass>(new TestClass() { A = 10, B = 1.23f, C = "Testing" }, "Item1"));
                Assert.Equal(new TestClass() { A = 10, B = 1.23f, C = "Testing" }, Bootstrapper.Resolve<TestClass>("Item1"));
            }
        }

        [Fact]
        public void RegisterGenericFunctionTest()
        {
            using (Batman.Core.Bootstrapper.TinyIoC.Bootstrapper Bootstrapper = new Core.Bootstrapper.TinyIoC.Bootstrapper())
            {
                Assert.DoesNotThrow(() => Bootstrapper.Register<TestClass>(() => new TestClass() { A = 10, B = 1.23f, C = "Testing" }));
                Assert.Equal(new TestClass() { A = 10, B = 1.23f, C = "Testing" }, Bootstrapper.Resolve<TestClass>());
            }
        }

        [Fact]
        public void RegisterGenericFunctionNamedTest()
        {
            using (Batman.Core.Bootstrapper.TinyIoC.Bootstrapper Bootstrapper = new Core.Bootstrapper.TinyIoC.Bootstrapper())
            {
                Assert.DoesNotThrow(() => Bootstrapper.Register<TestClass>(() => new TestClass() { A = 10, B = 1.23f, C = "Testing" },"Test1"));
                Assert.Equal(new TestClass() { A = 10, B = 1.23f, C = "Testing" }, Bootstrapper.Resolve<TestClass>("Test1"));
            }
        }
    }

    public class TestClass
    {
        public int A { get; set; }
        public float B { get; set; }
        public string C { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            TestClass Item = obj as TestClass;
            return (Item.A == A
                    && Item.B == B
                    && Item.C == C);
        }

        public override int GetHashCode()
        {
            return A.GetHashCode() + B.GetHashCode() + C.GetHashCode();
        }
    }
}

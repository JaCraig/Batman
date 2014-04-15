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
using Batman.Core.Bootstrapper.BaseClasses;
using Batman.Core.Bootstrapper.Interfaces;
using StructureMap;

#endregion Usings

namespace Batman.Core.Bootstrapper.TinyIoC
{
    /// <summary>
    /// StructureMap based bootstrapper
    /// </summary>
    public class Bootstrapper : BootstrapperBase<Container>
    {
        public Bootstrapper()
            : base()
        {
        }

        public override string Name { get { return "StructureMap"; } }

        protected override Container AppContainer { get { return new Container(); } }

        public override void Dispose(bool Managed)
        {
        }

        public override void Register<T>(Func<T> Function, string Name)
        {
            ObjectFactory.Initialize(x => x.For<T>().Use(Name, Function));
        }

        public override void Register<T1, T2>(string Name)
        {
            ObjectFactory.Initialize(x => x.For<T1>().Use<T2>());
        }

        public override void Register<T>(string Name)
        {
            ObjectFactory.Initialize(x => x.For<T>().Use<T>());
        }

        public override void Register<T>(T Object, string Name)
        {
            ObjectFactory.Initialize(x => x.For<T>().Use(Object));
        }

        public override void Register<T>(Func<T> Function)
        {
            ObjectFactory.Initialize(x => x.For<T>().Use("", Function));
        }

        public override void Register<T1, T2>()
        {
            ObjectFactory.Initialize(x => x.For<T1>().Use<T2>());
        }

        public override void Register<T>()
        {
            ObjectFactory.Initialize(x => x.For<T>().Use<T>());
        }

        public override void Register<T>(T Object)
        {
            ObjectFactory.Initialize(x => x.For<T>().Use(Object));
        }

        public override object Resolve(Type ObjectType, string Name, object DefaultObject = null)
        {
            try
            {
                return ObjectFactory.GetInstance(ObjectType);
            }
            catch { return DefaultObject; }
        }

        public override object Resolve(Type ObjectType, object DefaultObject = null)
        {
            try
            {
                return ObjectFactory.GetInstance(ObjectType);
            }
            catch { return DefaultObject; }
        }

        public override T Resolve<T>(string Name, T DefaultObject = default(T))
        {
            try
            {
                return ObjectFactory.GetInstance<T>();
            }
            catch { return DefaultObject; }
        }

        public override T Resolve<T>(T DefaultObject = default(T))
        {
            try
            {
                return ObjectFactory.GetInstance<T>();
            }
            catch { return DefaultObject; }
        }

        public override IEnumerable<object> ResolveAll(Type ObjectType)
        {
            try
            {
                return ObjectFactory.GetAllInstances(ObjectType).OfType<object>();
            }
            catch { return new List<object>(); }
        }

        public override IEnumerable<T> ResolveAll<T>()
        {
            try
            {
                return ObjectFactory.GetAllInstances<T>();
            }
            catch { return new List<T>(); }
        }
    }
}
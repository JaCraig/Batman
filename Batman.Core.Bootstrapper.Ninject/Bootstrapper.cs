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
using Batman.Core.Bootstrapper.Interfaces;
using Batman.Core.Bootstrapper.BaseClasses;
using System;
using System.Collections.Generic;
using Ninject;
#endregion

namespace Batman.Core.Bootstrapper.Ninject
{
    /// <summary>
    /// Ninject based bootstrapper
    /// </summary>
    public class Bootstrapper : BootstrapperBase<StandardKernel>
    {
        public Bootstrapper()
            : base()
        {
            _Container = new StandardKernel();
        }

        private StandardKernel _Container = null;

        protected override StandardKernel AppContainer { get { return _Container; } }

        public override string Name { get { return "Ninject"; } }

        public override void Register<T>(Func<T> Function, string Name)
        {
            AppContainer.Bind<T>().ToMethod(x => Function());
        }

        public override void Register<T1, T2>(string Name)
        {
            AppContainer.Bind<T1>().To<T2>();
        }

        public override void Register<T>(string Name)
        {
            AppContainer.Bind<T>().ToSelf();
        }

        public override void Register<T>(T Object, string Name)
        {
            AppContainer.Bind<T>().ToConstant(Object);
        }

        public override void Register<T>(Func<T> Function)
        {
            AppContainer.Bind<T>().ToMethod(x => Function());
        }

        public override void Register<T1, T2>()
        {
            AppContainer.Bind<T1>().To<T2>();
        }

        public override void Register<T>()
        {
            AppContainer.Bind<T>().ToSelf();
        }

        public override void Register<T>(T Object)
        {
            AppContainer.Bind<T>().ToConstant(Object);
        }

        public override object Resolve(Type ObjectType, string Name, object DefaultObject = null)
        {
            try
            {
                return AppContainer.Get(ObjectType);
            }
            catch { return DefaultObject; }
        }

        public override object Resolve(Type ObjectType, object DefaultObject = null)
        {
            try
            {
                return AppContainer.Get(ObjectType);
            }
            catch { return DefaultObject; }
        }

        public override T Resolve<T>(string Name, T DefaultObject = default(T))
        {
            try
            {
                return AppContainer.Get<T>();
            }
            catch { return DefaultObject; }
        }

        public override T Resolve<T>(T DefaultObject = default(T))
        {
            try
            {
                return AppContainer.Get<T>();
            }
            catch { return DefaultObject; }
        }

        public override IEnumerable<object> ResolveAll(Type ObjectType)
        {
            try
            {
                return AppContainer.GetAll(ObjectType);
            }
            catch { return new List<object>(); }
        }

        public override IEnumerable<T> ResolveAll<T>()
        {
            try
            {
                return AppContainer.GetAll<T>();
            }
            catch { return new List<T>(); }
        }

        public override void Dispose(bool Managed)
        {
            if (_Container != null)
            {
                _Container.Dispose();
                _Container = null;
            }
        }
    }
}
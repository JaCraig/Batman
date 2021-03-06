﻿/*
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

using Batman.Core.Bootstrapper.BaseClasses;
using System;
using System.Collections.Generic;
using TinyIoC;

namespace Batman.Core.Bootstrapper.TinyIoC
{
    /// <summary>
    /// TinyIoC based bootstrapper
    /// </summary>
    public class Bootstrapper : BootstrapperBase<TinyIoCContainer>
    {
        public override string Name { get { return "TinyIoC"; } }
        protected override TinyIoCContainer AppContainer { get { return TinyIoCContainer.Current; } }

        public override void Dispose(bool Managed)
        {
            AppContainer.Dispose();
        }

        public override void Register<T>(Func<T> Function, string Name)
        {
            AppContainer.Register<T>((x, y) => Function(), Name);
        }

        public override void Register<T1, T2>(string Name)
        {
            AppContainer.Register<T1, T2>(Name);
        }

        public override void Register<T>(string Name)
        {
            AppContainer.Register<T>(Name);
        }

        public override void Register<T>(T Object, string Name)
        {
            AppContainer.Register<T>(Object, Name);
        }

        public override void Register<T>(Func<T> Function)
        {
            AppContainer.Register<T>((x, y) => Function());
        }

        public override void Register<T1, T2>()
        {
            AppContainer.Register<T1, T2>();
        }

        public override void Register<T>()
        {
            AppContainer.Register<T>();
        }

        public override void Register<T>(T Object)
        {
            AppContainer.Register<T>(Object);
        }

        public override object Resolve(Type ObjectType, string Name, object DefaultObject = null)
        {
            try
            {
                return AppContainer.Resolve(ObjectType, Name);
            }
            catch { return DefaultObject; }
        }

        public override object Resolve(Type ObjectType, object DefaultObject = null)
        {
            try
            {
                return AppContainer.Resolve(ObjectType);
            }
            catch { return DefaultObject; }
        }

        public override T Resolve<T>(string Name, T DefaultObject = default(T))
        {
            try
            {
                return AppContainer.Resolve<T>(Name);
            }
            catch { return DefaultObject; }
        }

        public override T Resolve<T>(T DefaultObject = default(T))
        {
            try
            {
                return AppContainer.Resolve<T>();
            }
            catch { return DefaultObject; }
        }

        public override IEnumerable<object> ResolveAll(Type ObjectType)
        {
            try
            {
                return AppContainer.ResolveAll(ObjectType);
            }
            catch { return new List<object>(); }
        }

        public override IEnumerable<T> ResolveAll<T>()
        {
            try
            {
                return AppContainer.ResolveAll<T>();
            }
            catch { return new List<T>(); }
        }
    }
}
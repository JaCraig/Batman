/*
Copyright (c) 2012 <a href="http://www.gutgames.com">James Craig</a>

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
using Batman.Core.Bootstrapper.Interfaces;
using System.Collections.Generic;
#endregion

namespace Batman.Core.Bootstrapper.BaseClasses
{
    /// <summary>
    /// Bootstrapper base class
    /// </summary>
    /// <typeparam name="Container">The actual IoC object</typeparam>
    public abstract class BootstrapperBase<Container> : IBootstrapper
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        protected BootstrapperBase()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// The IoC container
        /// </summary>
        protected Container AppContainer { get; private set; }

        #endregion

        #region Functions

        /// <summary>
        /// Initializes the bootstrapper
        /// </summary>
        /// <param name="AppContainer">The IoC container used by the app</param>
        public void Initialize(Container AppContainer)
        {
            this.AppContainer = AppContainer;
        }

        /// <summary>
        /// Registers an object with the bootstrapper
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">Object to register</param>
        public abstract void Register<T>(T Object)
            where T : class;

        /// <summary>
        /// Registers a type with the default constructor
        /// </summary>
        /// <typeparam name="T">Object type to register</typeparam>
        public abstract void Register<T>()
            where T : class;

        /// <summary>
        /// Registers a type with the default constructor of a child class
        /// </summary>
        /// <typeparam name="T1">Base class/interface type</typeparam>
        /// <typeparam name="T2">Child class type</typeparam>
        public abstract void Register<T1, T2>()
            where T2 : class,T1
            where T1 : class;

        /// <summary>
        /// Registers a type with a function
        /// </summary>
        /// <typeparam name="T">Type that the function returns</typeparam>
        /// <param name="Function">Function to register with the type</param>
        public abstract void Register<T>(Func<T> Function)
            where T : class;

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>An object of the specified type</returns>
        public abstract T Resolve<T>()
            where T : class;

        /// <summary>
        /// Registers an object with the bootstrapper
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">Object to register</param>
        /// <param name="Name">Name associated with the object</param>
        public abstract void Register<T>(T Object,string Name)
            where T : class;

        /// <summary>
        /// Registers a type with the default constructor
        /// </summary>
        /// <typeparam name="T">Object type to register</typeparam>
        /// <param name="Name">Name associated with the object</param>
        public abstract void Register<T>(string Name)
            where T : class;

        /// <summary>
        /// Registers a type with the default constructor of a child class
        /// </summary>
        /// <typeparam name="T1">Base class/interface type</typeparam>
        /// <typeparam name="T2">Child class type</typeparam>
        /// <param name="Name">Name associated with the object</param>
        public abstract void Register<T1, T2>(string Name)
            where T2 : class,T1
            where T1 : class;

        /// <summary>
        /// Registers a type with a function
        /// </summary>
        /// <typeparam name="T">Type that the function returns</typeparam>
        /// <param name="Name">Name associated with the object</param>
        /// <param name="Function">Function to register with the type</param>
        public abstract void Register<T>(Func<T> Function, string Name)
            where T : class;

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="Name">Name associated with the object</param>
        /// <returns>An object of the specified type</returns>
        public abstract T Resolve<T>(string Name)
            where T : class;

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <returns>An object of the specified type</returns>
        public abstract object Resolve(Type ObjectType);

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Name">Name associated with the object</param>
        /// <returns>An object of the specified type</returns>
        public abstract object Resolve(Type ObjectType, string Name);

        /// <summary>
        /// Resolves the objects based on the type specified
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>A list of objects of the specified type</returns>
        public abstract IEnumerable<T> ResolveAll<T>()
            where T : class;

        /// <summary>
        /// Resolves all objects based on the type specified
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <returns>A list of objects of the specified type</returns>
        public abstract IEnumerable<object> ResolveAll(Type ObjectType);

        #endregion
    }
}
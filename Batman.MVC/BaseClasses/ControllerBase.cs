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
using Batman.Core.Bootstrapper.Interfaces;
using System.Web.Mvc;
using System.Collections.Generic;
using Batman.MVC.Assets.Interfaces;
using Batman.MVC.Assets.Enums;
using Batman.Core.FileSystem;
using Utilities.DataTypes.ExtensionMethods;
using System.Linq;
using System.Web.Optimization;
using System.IO;
using Batman.MVC.Assets.Utils;
using Utilities.Reflection.ExtensionMethods;
using Batman.Core.FileSystem.Interfaces;
using System.Web;
using Utilities.DataTypes;
using Batman.Core.Tasks.Interfaces;
using Batman.MVC.Assets;
using Batman.Core;
using System.Configuration;
using Batman.Core.Communication;
using Batman.Core.Communication.Interfaces;
#endregion

namespace Batman.MVC.BaseClasses
{
    /// <summary>
    /// Controller base class
    /// </summary>
    public abstract class ControllerBase : Controller
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        protected ControllerBase()
            : base()
        {
        }

        #endregion

        #region Functions

        /// <summary>
        /// Creates a message object
        /// </summary>
        /// <typeparam name="T">Message type</typeparam>
        /// <returns>The created message object</returns>
        protected T CreateMessage<T>()
            where T : class,IMessage
        {
            return DependencyResolver.Current.GetService<CommunicationManager>()[typeof(T)].CreateMessage() as T;
        }

        #endregion
    }
}
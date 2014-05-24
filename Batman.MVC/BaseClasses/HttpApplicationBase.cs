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

using Batman.Core;
using Batman.Core.Bootstrapper.Interfaces;
using Batman.Core.Communication;
using Batman.Core.Communication.Interfaces;
using Batman.Core.FileSystem;
using Batman.Core.FileSystem.Interfaces;
using Batman.Core.Logging.BaseClasses;
using Batman.Core.Profiling.Interfaces;
using Batman.Core.Tasks.Interfaces;
using Batman.MVC.Assets;
using Batman.MVC.Assets.Enums;
using Batman.MVC.Assets.Interfaces;
using Batman.MVC.Assets.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Utilities.DataTypes;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.Logging.Enums;

#endregion Usings

namespace Batman.MVC.BaseClasses
{
    /// <summary>
    /// HttpApplication base class that auto attaches a couple items (profiler, etc)
    /// </summary>
    public abstract class HttpApplicationBase : System.Web.HttpApplication
    {
        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                if (!UserCanProfile())
                {
                    DependencyResolver.Current.GetService<IProfiler>().Stop(true);
                }
            }
            catch { }
        }

        /// <summary>
        /// Called at the beginning of the application request
        /// </summary>
        protected virtual void Application_BeginRequest()
        {
            try
            {
                DependencyResolver.Current.GetService<IProfiler>().Start();
            }
            catch { }
        }

        /// <summary>
        /// Called at the end of the application request
        /// </summary>
        protected virtual void Application_EndRequest()
        {
            try
            {
                DependencyResolver.Current.GetService<IProfiler>().Stop(false);
            }
            catch { }
        }

        /// <summary>
        /// Called when the application has an error
        /// </summary>
        protected virtual void Application_Error()
        {
            try
            {
                Response.Filter = null;
            }
            catch { }
        }

        /// <summary>
        /// Determines if the user can see profiling data
        /// </summary>
        /// <returns>True if they can see profiling data, false otherwise</returns>
        protected virtual bool UserCanProfile()
        {
            return Request.IsLocal;
        }
    }
}
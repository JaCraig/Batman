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
using System.Text;
using Batman.Core.Bootstrapper.Interfaces;
using Utilities.Reflection.ExtensionMethods;
using Utilities.DataTypes.ExtensionMethods;
using Batman.Core.Logging.BaseClasses;
using Utilities.IO.Logging.Enums;
using Batman.Core.Logging;
#endregion

namespace Batman.Core
{
    /// <summary>
    /// Generally controls the basic flow of the application during certain phases
    /// </summary>
    public static class BatComputer
    {
        #region Members

        /// <summary>
        /// Bootstrapper that the application holds onto
        /// </summary>
        public static IBootstrapper Bootstrapper = null;

        public static LogBase Logger = null;

        #endregion

        #region Functions

        /// <summary>
        /// Called at the start of the application
        /// </summary>
        public static void Start()
        {
            Bootstrapper = AppDomain.CurrentDomain.GetAssemblies().GetObjects<IBootstrapper>().FirstOrDefault();
            AppDomain.CurrentDomain.GetAssemblies().GetObjects<IModule>().ForEach(x => x.Load(Bootstrapper));
            Logger = Bootstrapper.Resolve<LogBase>(new NullLogger());
            Logger.LogMessage("Application starting", MessageType.Info);
        }

        /// <summary>
        /// Called at the end of the application
        /// </summary>
        public static void End()
        {
            if (Logger != null)
            {
                Logger.LogMessage("Application ending", MessageType.Info);
                Logger.Dispose();
                Logger = null;
            }
            if (Bootstrapper != null)
            {
                Bootstrapper.Dispose();
                Bootstrapper = null;
            }
        }

        #endregion
    }
}
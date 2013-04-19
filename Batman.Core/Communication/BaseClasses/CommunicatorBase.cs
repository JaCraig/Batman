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
using System.IO;
using Batman.Core.Tasks;
using Batman.Core.Tasks.Enums;
using Batman.Core.FileSystem;
using Batman.Core.Communication.Interfaces;
using Batman.Core.FileSystem.Interfaces;
using System.Threading;
#endregion

namespace Batman.Core.Communication.BaseClasses
{
    /// <summary>
    /// Communicator base class
    /// </summary>
    public abstract class CommunicatorBase : ICommunicator
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        protected CommunicatorBase()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of the communicator
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Message type used by the communicator
        /// </summary>
        public abstract Type MessageType { get; }

        /// <summary>
        /// Formatters
        /// </summary>
        public IEnumerable<IFormatter> Formatters { get; private set; }

        #endregion

        #region Functions

        /// <summary>
        /// Initializes the communicator
        /// </summary>
        /// <param name="Formatters">Formatters to use</param>
        /// <returns>this</returns>
        public ICommunicator Initialize(IEnumerable<IFormatter> Formatters)
        {
            this.Formatters = Formatters;
            return this;
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <param name="Message">Message to send</param>
        /// <returns>this</returns>
        public ICommunicator Send<T>(IMessage Message, string Template, T Model)
        {
            foreach (IFormatter Formatter in Formatters)
            {
                Formatter.Format(Message, Template, Model);
            }
            InternalSend(Message);
            return this;
        }

        /// <summary>
        /// Sends a message asynchronously
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <param name="Message">Message to send</param>
        /// <returns>this</returns>
        public ICommunicator SendAsync<T>(IMessage Message, string Template, T Model)
        {
            ThreadPool.QueueUserWorkItem(x => Send(Message, Template, Model));
            return this;
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <param name="Message">Message to send</param>
        /// <returns>this</returns>
        public ICommunicator Send<T>(IMessage Message, IFile Template, T Model)
        {
            foreach (IFormatter Formatter in Formatters)
            {
                Formatter.Format(Message, Template, Model);
            }
            InternalSend(Message);
            return this;
        }

        /// <summary>
        /// Sends a message asynchronously
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <param name="Message">Message to send</param>
        /// <returns>this</returns>
        public ICommunicator SendAsync<T>(IMessage Message, IFile Template, T Model)
        {
            ThreadPool.QueueUserWorkItem(x => Send(Message, Template, Model));
            return this;
        }

        /// <summary>
        /// Creates a message
        /// </summary>
        /// <returns>Message object</returns>
        public abstract IMessage CreateMessage();

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="Message">Message to send</param>
        protected abstract void InternalSend(IMessage Message);

        #endregion
    }
}
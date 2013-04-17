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
    /// Message base class
    /// </summary>
    public abstract class MessageBase : IMessage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Communicator">Communicator used to create the message</param>
        protected MessageBase(ICommunicator Communicator)
        {
            this.Communicator = Communicator;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Communicator used for sending the message
        /// </summary>
        private ICommunicator Communicator { get; set; }

        /// <summary>
        /// Whom the message is to
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// The subject of the Communicator
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Whom the message is from
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Body of the text
        /// </summary>
        public string Body { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <returns>this</returns>
        public IMessage Send<T>(string Template, T Model)
        {
            Communicator.Send(this, Template, Model);
            return this;
        }

        /// <summary>
        /// Sends a message asynchronously
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <returns>this</returns>
        public IMessage SendAsync<T>(string Template, T Model)
        {
            Communicator.SendAsync(this, Template, Model);
            return this;
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <returns>this</returns>
        public IMessage Send<T>(IFile Template, T Model)
        {
            Communicator.Send(this, Template, Model);
            return this;
        }

        /// <summary>
        /// Sends a message asynchronously
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <returns>this</returns>
        public IMessage SendAsync<T>(IFile Template, T Model)
        {
            Communicator.Send(this, Template, Model);
            return this;
        }

        #endregion
    }
}
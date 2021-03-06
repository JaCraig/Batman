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

#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Batman.Core.Bootstrapper.Interfaces;

using Utilities.DataTypes.ExtensionMethods;
using Batman.Core.Logging.BaseClasses;
using Utilities.IO.Logging.Enums;
using Batman.Core.Logging;
using System.IO;
using Batman.Core.Tasks;
using Batman.Core.Tasks.Enums;
using Batman.Core.FileSystem;
using Batman.Core.FileSystem.Interfaces;
#endregion

namespace Batman.Core.Communication.Interfaces
{
    /// <summary>
    /// Communicator interface
    /// </summary>
    public interface ICommunicator
    {
        #region Properties

        /// <summary>
        /// Name of the communicator
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Message type used by the communicator
        /// </summary>
        Type MessageType { get; }

        #endregion

        #region Functions

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <param name="Message">Message to send</param>
        /// <returns>this</returns>
        ICommunicator Send<T>(IMessage Message, string Template, T Model);

        /// <summary>
        /// Sends a message asynchronously
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <param name="Message">Message to send</param>
        /// <returns>this</returns>
        ICommunicator SendAsync<T>(IMessage Message, string Template, T Model);

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <param name="Message">Message to send</param>
        /// <returns>this</returns>
        ICommunicator Send<T>(IMessage Message, IFile Template, T Model);

        /// <summary>
        /// Sends a message asynchronously
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template text to use</param>
        /// <param name="Model">Model object</param>
        /// <param name="Message">Message to send</param>
        /// <returns>this</returns>
        ICommunicator SendAsync<T>(IMessage Message, IFile Template, T Model);

        /// <summary>
        /// Creates a message
        /// </summary>
        /// <returns>Message object</returns>
        IMessage CreateMessage();

        #endregion
    }
}

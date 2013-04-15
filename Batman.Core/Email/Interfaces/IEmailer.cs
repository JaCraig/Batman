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
#endregion

namespace Batman.Core.Email.Interfaces
{
    /// <summary>
    /// Emailer interface
    /// </summary>
    public interface IEmailer
    {
        #region Functions

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="Messages">The messages to send</param>
        void SendMail(params IMessage[] Messages);

        /// <summary>
        /// Sends a piece of mail asynchronous
        /// </summary>
        /// <param name="Messages">The messages to send</param>
        void SendMailAsync(params IMessage[] Messages);

        /// <summary>
        /// Creates a message object
        /// </summary>
        /// <param name="Formatters">Formatters</param>
        /// <returns>The created message object</returns>
        IMessage CreateMessage(IEnumerable<IFormatter> Formatters,EmailManager Manager);

        #endregion
    }
}

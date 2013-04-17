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
using System.Net.Mail;
#endregion

namespace Batman.Core.Communication.Interfaces
{
    /// <summary>
    /// Email message interface
    /// </summary>
    public interface IEmailMessage : IMessage, IDisposable
    {
        #region Properties

        /// <summary>
        /// Any attachments that are included with this
        /// message.
        /// </summary>
        ICollection<Attachment> Attachments { get; }

        /// <summary>
        /// Any attachment (usually images) that need to be embedded in the message
        /// </summary>
        ICollection<LinkedResource> EmbeddedResources { get; }

        /// <summary>
        /// The priority of this message
        /// </summary>
        MailPriority Priority { get; set; }

        /// <summary>
        /// Server Location
        /// </summary>
        string Server { get; set; }

        /// <summary>
        /// User Name for the server
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Password for the server
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Port to send the information on
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Decides whether we are using STARTTLS (SSL) or not
        /// </summary>
        bool UseSSL { get; set; }

        /// <summary>
        /// Carbon copy send (seperate email addresses with a comma)
        /// </summary>
        string CC { get; set; }

        /// <summary>
        /// Blind carbon copy send (seperate email addresses with a comma)
        /// </summary>
        string Bcc { get; set; }

        #endregion
    }
}
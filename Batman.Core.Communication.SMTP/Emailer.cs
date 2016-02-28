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

using Batman.Core.Communication.BaseClasses;
using System;
using System.Net.Mail;
using System.Net.Mime;

namespace Batman.Core.Communication.SMTP
{
    /// <summary>
    /// SMTP emailer
    /// </summary>
    public class Emailer : CommunicatorBase
    {
        public override Type MessageType { get { return typeof(EmailMessage); } }
        public override string Name { get { return "SMTP"; } }

        public override Interfaces.IMessage CreateMessage()
        {
            return new EmailMessage(this);
        }

        protected override void InternalSend(Interfaces.IMessage message)
        {
            var Message = message as EmailMessage;
            if (Message == null)
                return;
            if (string.IsNullOrEmpty(Message.Body))
                Message.Body = " ";
            using (MailMessage TempMailMessage = new MailMessage())
            {
                char[] Splitter = { ',', ';' };
                string[] AddressCollection = Message.To.Split(Splitter);
                for (int x = 0; x < AddressCollection.Length; ++x)
                {
                    if (!string.IsNullOrEmpty(AddressCollection[x].Trim()))
                        TempMailMessage.To.Add(AddressCollection[x]);
                }
                if (!string.IsNullOrEmpty(Message.CC))
                {
                    AddressCollection = Message.CC.Split(Splitter);
                    for (int x = 0; x < AddressCollection.Length; ++x)
                    {
                        if (!string.IsNullOrEmpty(AddressCollection[x].Trim()))
                            TempMailMessage.CC.Add(AddressCollection[x]);
                    }
                }
                if (!string.IsNullOrEmpty(Message.Bcc))
                {
                    AddressCollection = Message.Bcc.Split(Splitter);
                    for (int x = 0; x < AddressCollection.Length; ++x)
                    {
                        if (!string.IsNullOrEmpty(AddressCollection[x].Trim()))
                            TempMailMessage.Bcc.Add(AddressCollection[x]);
                    }
                }
                TempMailMessage.Subject = Message.Subject;
                if (!string.IsNullOrEmpty(Message.From))
                    TempMailMessage.From = new MailAddress(Message.From);
                using (AlternateView BodyView = AlternateView.CreateAlternateViewFromString(Message.Body, null, MediaTypeNames.Text.Html))
                {
                    foreach (LinkedResource Resource in Message.EmbeddedResources)
                    {
                        BodyView.LinkedResources.Add(Resource);
                    }
                    TempMailMessage.AlternateViews.Add(BodyView);
                    TempMailMessage.Priority = Message.Priority;
                    TempMailMessage.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                    TempMailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                    TempMailMessage.IsBodyHtml = true;
                    foreach (Attachment TempAttachment in Message.Attachments)
                    {
                        TempMailMessage.Attachments.Add(TempAttachment);
                    }
                    if (!string.IsNullOrEmpty(Message.Server))
                    {
                        SendMessage(new SmtpClient(Message.Server, Message.Port), Message, TempMailMessage);
                    }
                    else
                    {
                        SendMessage(new SmtpClient(), Message, TempMailMessage);
                    }
                }
            }
        }

        private void SendMessage(SmtpClient smtpClient, EmailMessage Message, MailMessage message)
        {
            using (SmtpClient smtp = smtpClient)
            {
                if (!string.IsNullOrEmpty(Message.UserName) && !string.IsNullOrEmpty(Message.Password))
                {
                    smtp.Credentials = new System.Net.NetworkCredential(Message.UserName, Message.Password);
                }
                if (Message.UseSSL)
                    smtp.EnableSsl = true;
                else
                    smtp.EnableSsl = false;
                smtp.Send(message);
            }
        }
    }
}
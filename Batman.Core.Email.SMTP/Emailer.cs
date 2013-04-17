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
using Batman.Core.Email.Interfaces;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Collections.Generic;
using Batman.Core.Communication.BaseClasses;
#endregion

namespace Batman.Core.Communication.SMTP
{
    /// <summary>
    /// SMTP emailer
    /// </summary>
    public class Emailer : CommunicatorBase
    {
        public Emailer() { }

        public void SendMail(params IMessage[] Messages)
        {
            foreach (IMessage Message in Messages)
            {
                using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
                {
                    char[] Splitter = { ',', ';' };
                    string[] AddressCollection = Message.To.Split(Splitter);
                    for (int x = 0; x < AddressCollection.Length; ++x)
                    {
                        if (!string.IsNullOrEmpty(AddressCollection[x].Trim()))
                            message.To.Add(AddressCollection[x]);
                    }
                    if (!string.IsNullOrEmpty(Message.CC))
                    {
                        AddressCollection = Message.CC.Split(Splitter);
                        for (int x = 0; x < AddressCollection.Length; ++x)
                        {
                            if (!string.IsNullOrEmpty(AddressCollection[x].Trim()))
                                message.CC.Add(AddressCollection[x]);
                        }
                    }
                    if (!string.IsNullOrEmpty(Message.Bcc))
                    {
                        AddressCollection = Message.Bcc.Split(Splitter);
                        for (int x = 0; x < AddressCollection.Length; ++x)
                        {
                            if (!string.IsNullOrEmpty(AddressCollection[x].Trim()))
                                message.Bcc.Add(AddressCollection[x]);
                        }
                    }
                    message.Subject = Message.Subject;
                    message.From = new System.Net.Mail.MailAddress(Message.From);
                    using (AlternateView BodyView = AlternateView.CreateAlternateViewFromString(Message.Body, null, MediaTypeNames.Text.Html))
                    {
                        foreach (LinkedResource Resource in Message.EmbeddedResources)
                        {
                            BodyView.LinkedResources.Add(Resource);
                        }
                        message.AlternateViews.Add(BodyView);
                        message.Priority = Message.Priority;
                        message.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                        message.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                        message.IsBodyHtml = true;
                        foreach (Attachment TempAttachment in Message.Attachments)
                        {
                            message.Attachments.Add(TempAttachment);
                        }
                        using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(Message.Server, Message.Port))
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
        }

        public void SendMailAsync(params IMessage[] Messages)
        {
            ThreadPool.QueueUserWorkItem(delegate { SendMail(Messages); });
        }

        public IMessage CreateMessage(IEnumerable<IFormatter> Formatters, EmailManager Manager)
        {
            return new DefaultMessage(Formatters, Manager);
        }

        public override string Name
        {
            get { throw new System.NotImplementedException(); }
        }

        public override Interfaces.IMessage CreateMessage()
        {
            throw new System.NotImplementedException();
        }

        protected override void InternalSend(Interfaces.IMessage Message)
        {
            throw new System.NotImplementedException();
        }
    }
}
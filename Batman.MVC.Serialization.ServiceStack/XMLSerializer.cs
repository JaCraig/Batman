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
using System.Web.Mvc;
using System.Collections.Generic;
using Batman.MVC.Assets.Interfaces;
using Batman.MVC.Assets.Enums;
using System.Linq;
using System.IO;
using Batman.MVC.Assets.Utils;
using System.Web;
using Batman.MVC.Assets;
using Batman.Core;
using Batman.Core.Serialization.Interfaces;
using System.Text;
#endregion

namespace Batman.MVC.Serialization.ServiceStackSerializers
{
    /// <summary>
    /// ServiceStack based XML serializer
    /// </summary>
    public class XMLSerializer : ISerializer
    {
        public string ContentType { get { return "text/xml"; } }

        public string Name { get { return "ServiceStack.XML"; } }

        public string FileType { get { return ".xml"; } }

        public ActionResult Serialize(Type ObjectType, object Data)
        {
            if (Data == null)
                return new ContentResult();
            ContentResult Result = new ContentResult();
            using (MemoryStream Stream = new MemoryStream())
            {
                ServiceStack.Text.XmlSerializer.SerializeToStream(Data, Stream);
                Result.Content = Encoding.ASCII.GetString(Stream.ToArray());
                Result.ContentType = ContentType;
                return Result;
            }
        }


        public ActionResult Serialize<T>(T Data)
        {
            if (Data == null)
                return new ContentResult();
            ContentResult Result = new ContentResult();
            Result.Content = ServiceStack.Text.XmlSerializer.SerializeToString(Data);
            Result.ContentType = ContentType;
            return Result;
        }
    }
}
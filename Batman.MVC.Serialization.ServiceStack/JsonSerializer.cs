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
#endregion

namespace Batman.MVC.Serialization.ServiceStackSerializers
{
    /// <summary>
    /// ServiceStack based Json serializer
    /// </summary>
    public class JsonSerializer : ISerializer
    {
        public string ContentType { get { return "application/json"; } }

        public string Name { get { return "ServiceStack.JSON"; } }

        public string FileType { get { return ".json"; } }

        public ActionResult Serialize(Type ObjectType, object Data)
        {
            if (Data == null)
                return new ContentResult();
            var Result = new ContentResult();
            Result.Content = ServiceStack.Text.JsonSerializer.SerializeToString(Data, ObjectType);
            HttpRequest Request = HttpContext.Current.Request;
            if (!string.IsNullOrEmpty(Request.QueryString["callback"]) || !string.IsNullOrEmpty(Request.QueryString["jsonp"]))
            {
                string Callback = Request.QueryString["callback"] ?? Request.QueryString["jsonp"];
                Result.Content = string.Format("{0}({1});", Callback, Result.Content);
            }
            Result.ContentType = ContentType;
            return Result;
        }

        public ActionResult Serialize<T>(T Data)
        {
            if (Data == null)
                return new ContentResult();
            var Result = new ContentResult();
            Result.Content = ServiceStack.Text.JsonSerializer.SerializeToString(Data);
            HttpRequest Request = HttpContext.Current.Request;
            if (!string.IsNullOrEmpty(Request.QueryString["callback"]) || !string.IsNullOrEmpty(Request.QueryString["jsonp"]))
            {
                string Callback = Request.QueryString["callback"] ?? Request.QueryString["jsonp"];
                Result.Content = string.Format("{0}({1});", Callback, Result.Content);
            }
            Result.ContentType = ContentType;
            return Result;
        }
    }
}
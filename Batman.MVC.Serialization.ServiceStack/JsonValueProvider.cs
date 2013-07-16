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
using Batman.MVC.VPFactories.BaseClasses;
using Utilities.IO.ExtensionMethods;
using Utilities.DataTypes.ExtensionMethods;
using System.Dynamic;
using System.Globalization;
#endregion

namespace Batman.MVC.Serialization.ServiceStackSerializers
{
    /// <summary>
    /// Acts as a JSON ValueProviderFactory that uses JSON.Net
    /// </summary>
    public class JsonValueProvider : VPFactoryBase
    {
        /// <summary>
        /// Gets the value provider
        /// </summary>
        /// <param name="controllerContext">Controller context</param>
        /// <returns>The value provider</returns>
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            HttpRequestBase Request = controllerContext.HttpContext.Request;
            if (!Request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
                return null;
            string Body = Request.InputStream.ReadAll();
            return Body.Is(x => string.IsNullOrEmpty(x)) ? null : new DictionaryValueProvider<object>(ServiceStack.Text.JsonSerializer.DeserializeFromString<ExpandoObject>(Body), CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Adds the factory to the system
        /// </summary>
        public override void AddFactory()
        {
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonValueProvider());
        }
    }
}

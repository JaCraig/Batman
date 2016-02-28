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

using Batman.MVC.Assets.Enums;
using Batman.MVC.Assets.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Yahoo.Yui.Compressor;

namespace Batman.MVC.Assets.Yui
{
    /// <summary>
    /// CSS minifier
    /// </summary>
    public class CSSMinifier : IFilter
    {
        public string Name { get { return "YUI CSS Minifier"; } }

        public RunTime TimeToRun
        {
            get { return RunTime.Minify; }
        }

        public IList<IAsset> Filter(IList<IAsset> Assets)
        {
            if (Assets == null || Assets.Count == 0)
                return new List<IAsset>();
            if (Assets.FirstOrDefault().Type != AssetType.CSS)
                return Assets;
            IEnumerable<IAsset> Processable = Assets.Where(x => !x.Minified);
            if (Processable.FirstOrDefault() == null)
                return Assets;
            var Minifier = new CssCompressor { CompressionType = CompressionType.Standard, RemoveComments = true };
            foreach (IAsset Asset in Processable)
            {
                try
                {
                    Asset.Content = Minifier.Compress(Asset.Content);
                    Asset.Minified = true;
                }
                catch { }
            }
            return Assets;
        }
    }
}
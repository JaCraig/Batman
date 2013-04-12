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
using Batman.Core.Bootstrapper.Interfaces;
using System.Web.Mvc;
using System.Collections.Generic;
using Batman.Core.MVC.Assets.Interfaces;
using Batman.Core.MVC.Assets.Enums;
using Batman.Core.FileSystem;
using Utilities.DataTypes.ExtensionMethods;
using System.Linq;
using System.Text.RegularExpressions;
using System.Drawing;
using Utilities.Media.Image.ExtensionMethods;
using System.Drawing.Imaging;
using Batman.Core.FileSystem.Interfaces;
#endregion

namespace Batman.Core.MVC.Assets.Filters
{
    /// <summary>
    /// Embeds the images and fonts that are linked from the CSS file
    /// </summary>
    public class CSSFixImagesAndFonts : IFilter
    {
        /// <summary>
        /// Time to run the filter
        /// </summary>
        public RunTime TimeToRun { get { return RunTime.PreCombine; } }

        /// <summary>
        /// Used to determine images in the CSS file
        /// </summary>
        private Regex ImageRegex = new Regex(@"url\([""']*(?<File>[^""')]*)[""']*\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Filters the assets
        /// </summary>
        /// <param name="Assets">Assets to filter</param>
        /// <returns>The filtered assets</returns>
        public IList<IAsset> Filter(IList<IAsset> Assets)
        {
            if (Assets == null || Assets.Count == 0)
                return new List<IAsset>();
            if (Assets.FirstOrDefault().Type != AssetType.CSS)
                return Assets;
            FileManager FileSystem = BatComputer.Bootstrapper.Resolve<FileManager>();
            foreach (IAsset Asset in Assets)
            {
                foreach (Match ImageMatch in ImageRegex.Matches(Asset.Content))
                {
                    string TempFile = ImageMatch.Groups["File"].Value;
                    string MatchString = ImageMatch.Value;
                    IFile File = FileSystem.File(TempFile);
                    if (File == null || !File.Exists)
                    {
                        IFile AssetFile = FileSystem.File(Asset.Path);
                        File = FileSystem.File(AssetFile.Directory.FullName + "\\" + TempFile);
                    }

                    if (File.Extension.ToUpperInvariant() == ".TTF"
                        || File.Extension.ToUpperInvariant() == ".OTF"
                        || File.Extension.ToUpperInvariant() == ".WOFF"
                        || File.Extension.ToUpperInvariant() == ".SVG"
                        || File.Extension.ToUpperInvariant() == ".EOT")
                    {
                        string MIME ="";
                        if (File.Extension.ToUpperInvariant() == ".WOFF")
                            MIME = "application/x-font-woff";
                        else if (File.Extension.ToUpperInvariant() == ".OTF")
                            MIME = "application/x-font-opentype";
                        else if (File.Extension.ToUpperInvariant() == ".TTF")
                            MIME = "application/x-font-ttf";
                        else if (File.Extension.ToUpperInvariant() == ".SVG")
                            MIME = "image/svg+xml";
                        else if (File.Extension.ToUpperInvariant() == ".EOT")
                            MIME = "application/vnd.ms-fontobject";

                        Asset.Content = Asset.Content.Replace(MatchString, "url(data:" + MIME + ";base64," + File.ReadBinary().ToBase64String() + ")");
                    }
                    else
                    {
                        using (Bitmap TempImage = new Bitmap(File.FullName))
                        {
                            Asset.Content = Asset.Content.Replace(MatchString, "url(data:image/png;base64," + TempImage.ToBase64(ImageFormat.Png) + ")");
                        }
                    }
                }
            }
            return Assets;
        }
    }
}
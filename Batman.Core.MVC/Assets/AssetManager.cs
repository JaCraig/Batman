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
using Batman.Core.Bootstrapper.Interfaces;
using System.Web.Mvc;
using System.Collections.Generic;
using Batman.Core.MVC.Assets.Interfaces;
using Batman.Core.MVC.Assets.Enums;
using Batman.Core.FileSystem;
#endregion

namespace Batman.Core.MVC.Assets
{
    /// <summary>
    /// Asset manager class
    /// </summary>
    public class AssetManager
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AssetManager()
        {
            Filters = new List<IFilter>();
            Minifiers = new List<IMinifier>();
            Orderers = new List<IOrderer>();
            Transformers = new List<ITransformer>();
            Translators = new List<ITranslator>();
            Validators = new List<IValidator>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Filters
        /// </summary>
        protected IList<IFilter> Filters { get; private set; }

        /// <summary>
        /// Minifiers
        /// </summary>
        protected IList<IMinifier> Minifiers { get; private set; }

        /// <summary>
        /// Orderers
        /// </summary>
        protected IList<IOrderer> Orderers { get; private set; }

        /// <summary>
        /// Transformers
        /// </summary>
        protected IList<ITransformer> Transformers { get; private set; }

        /// <summary>
        /// Translators
        /// </summary>
        protected IList<ITranslator> Translators { get; private set; }

        /// <summary>
        /// Validators
        /// </summary>
        protected IList<IValidator> Validators { get; private set; }

        #endregion

        #region Functions

        /// <summary>
        /// Determines the asset type
        /// </summary>
        /// <param name="Path">Path to the asset</param>
        /// <returns>The asset type</returns>
        public AssetType DetermineType(string Path)
        {
            return AssetType.Unknown;
        }

        #endregion
    }
}
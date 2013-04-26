[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Batman.Core.AppHelper), "PreStart")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Batman.Core.AppHelper), "PostStart")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Batman.Core.AppHelper), "End")]

namespace Batman.Core
{
    #region Usings

    using System.Web.Mvc;
    using Batman.Core.Bootstrapper.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Batman.Core.Logging.BaseClasses;
    using Batman.Core.Logging;
    using Utilities.IO.Logging.Enums;
    using Batman.MVC.Assets;
    using Batman.Core.Serialization;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public static class AppHelper
    {
        public static void PreStart()
        {
            BatComputer.PreStart();
            LogBase Logger = BatComputer.Bootstrapper.Resolve<LogBase>(new NullLogger());
            Logger.LogMessage("Current asset helpers: {0}", MessageType.Debug, BatComputer.Bootstrapper.Resolve<AssetManager>().ToString());
            Logger.LogMessage("Current serializers: {0}", MessageType.Debug, BatComputer.Bootstrapper.Resolve<SerializationManager>().ToString());
            DependencyResolver.SetResolver(new Bootstrapper.DependencyResolver(BatComputer.Bootstrapper));
        }

        public static void PostStart()
        {
            BatComputer.PostStart();
        }

        public static void End()
        {
            BatComputer.End();
        }
    }
}
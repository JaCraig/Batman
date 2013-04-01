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

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public static class AppHelper
    {
        public static void PreStart()
        {
            BatComputer.PreStart();
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
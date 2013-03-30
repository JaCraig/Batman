[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Batman.Core.AppHelper), "Start")]
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

    public static class AppHelper
    {
        public static void Start()
        {
            BatComputer.Start();
            DependencyResolver.SetResolver(new Bootstrapper.DependencyResolver(BatComputer.Bootstrapper));
        }

        public static void End()
        {
            BatComputer.End();
        }
    }
}
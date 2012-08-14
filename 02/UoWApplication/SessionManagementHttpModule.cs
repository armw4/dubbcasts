using System;
using System.Web;
using UoW.NHibernate;

namespace UoWApplication
{
    public class SessionManagementHttpModule : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.BeginRequest += BeginRequest;
            application.PostLogRequest += PostLogRequest;
        }

        private static void BeginRequest(object sender, EventArgs e)
        {
            SessionFactory.BeginRequest();
        }

        /* use PostLogRequest instead of EndRequest since Autofac disposes of IDisposable instances like ISession during the EndRequest event.
         * we don't want to risk being too late and the session already being closed by the time we want to save our changes and commit our Unit of Work/transaction.
         * 
         * Autofac dynamically registers an HttpModule at application start that ends the lifetimescope in the EndRequest event.
         * you'll notice this module is making use of DynamicModuleUtility which allows you to circumvent web.cofig and register your
         * module in code.
         * 
         * http://code.google.com/p/autofac/source/browse/src/Source/Autofac.Integration.Mvc/PreApplicationStartCode.cs
         * http://code.google.com/p/autofac/source/browse/src/Source/Autofac.Integration.Mvc/RequestLifetimeHttpModule.cs
         */
        private static void PostLogRequest(object sender, EventArgs e)
        {
            SessionFactory.EndRequest();
        }

        public void Dispose()
        {
        }
    }
}
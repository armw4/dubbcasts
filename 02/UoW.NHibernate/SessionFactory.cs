using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using Environment = NHibernate.Cfg.Environment;

namespace UoW.NHibernate
{
    public static class SessionFactory
    {
        // A static constructor is used to initialize any static data, or to perform a particular action that needs performed once only.
        // It is called automatically before the first instance is created or any static members are referenced.
        static SessionFactory()
        {
            var sessionFactory = Fluently
                                .Configure()
                                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("Test"))) // present in Web.config
                                .Mappings(m =>
                                {
                                    var configuration = new UoWConfiguration();
                                    var autoPersistenceModel = AutoMap.AssemblyOf<User>(configuration); // tell NHibernate which types in this assembly are actual entities.

                                    m.AutoMappings.Add(autoPersistenceModel);
                                })
                                .ExposeConfiguration(cfg => cfg.SetProperty(Environment.CurrentSessionContextClass, "web")) // very important. the app won't work w/o this setting
                                .BuildSessionFactory();

            Current = sessionFactory;
        }

        public static ISessionFactory Current { get; private set; }

        public static void BeginRequest()
        {
            var sessionFactory = Current;
            var session = sessionFactory.OpenSession();

            session.BeginTransaction();

            CurrentSessionContext.Bind(session);
        }

        public static void EndRequest()
        {
            var sessionFactory = Current;
            var session = CurrentSessionContext.Unbind(sessionFactory);

            FinalizeSession(session);
        }

        private static void FinalizeSession(ISession session)
        {
            if (session == null || !session.IsOpen)
                return;

            try
            {
                if (session.Transaction != null && session.Transaction.IsActive)
                {
                    session.Flush();
                    session.Transaction.Commit();
                }
            }
            catch (Exception)
            {
                if (session.Transaction != null)
                    session.Transaction.Rollback();

                // you'll want to log the error here or allow it to bubble up to the global error trap in Application_Error of Global.asax.
                throw;
            }
            finally
            {
                session.Close();
                session.Dispose();
            }
        }
    }
}

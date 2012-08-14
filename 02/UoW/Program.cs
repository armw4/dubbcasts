using System;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernatePerformanceTip.Entities;

namespace NHibernatePerformanceTip
{
    class Program
    {
        private static ISessionFactory _sessionFactory;

        static void Main()
        {
            ConfigureMappings();

            Action slowRequest = SimulateMultipleWritesPerRequest;
            Action fastRequest = SimulateUnitOfWork;

            slowRequest.Time();
            fastRequest.Time();
        }

        public static void SimulateMultipleWritesPerRequest()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                1000.Times(() =>
                {
                    session.ExecuteTransaction(() =>
                    {
                        var user = new User
                        {
                            FirstName = "Antwan",
                            LastName = "Obama"
                        };

                        session.Save(user);
                    });
                });
            }
        }

        private static void SimulateUnitOfWork()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.ExecuteTransaction(() =>
                {
                    1000.Times(() =>
                    {
                        var user = new User
                        {
                            FirstName = "Antwan",
                            LastName = "Obama"
                        };

                        session.Save(user);
                    });
                });
            }
        }

        private static void ConfigureMappings()
        {
            NHibernateProfiler.Initialize();

            Action<MsSqlConnectionStringBuilder> connectionStringDelegate = c => c.FromConnectionStringWithKey("Test");

            var autoPersistenceModel = AutoMap.AssemblyOf<User>();

            _sessionFactory = Fluently
                                .Configure()
                                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionStringDelegate))
                                .Mappings(m =>
                                {
                                    m.AutoMappings.Add(autoPersistenceModel);
                                    m.HbmMappings.AddFromAssembly(Assembly.GetExecutingAssembly());
                                })
                                .ExposeConfiguration(cfg => cfg.SetProperty("generate_statistics", "true"))
                                .BuildSessionFactory();
        }
    }
}

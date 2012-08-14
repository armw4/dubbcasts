using Autofac;
using NHibernate;
using UoW.NHibernate;

namespace UoWApplication
{
    public class UoWModule : Module
    {
        protected override void Load(ContainerBuilder container)
        {
            container.Register(builder => SessionFactory.Current)
            .As<ISessionFactory>()
            .SingleInstance(); // very important. we only want one instance of the SessionFactory throughout the lifetime of the application as it's expensive to build.

            // very important. we need to get the session via SessionFactory.GetCurrentSession which is established in BeginRequest of SessionManagementHttpModule and SessionFactory.cs.
            container.Register(builder => builder.Resolve<ISessionFactory>().GetCurrentSession()).As<ISession>();
        }
    }
}
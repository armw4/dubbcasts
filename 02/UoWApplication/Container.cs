using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using UoW.NHibernate;
using UoWApplication.Controllers;

namespace UoWApplication
{
    public static class Container
    {
        public static void Configure()
        {
            var container = new ContainerBuilder();

            container.RegisterAssemblyTypes(typeof(IEntity).Assembly).AsImplementedInterfaces();

            container.RegisterControllers(typeof(AccountController).Assembly);

            container.RegisterModule(new UoWModule());
            container.RegisterModule(new AutofacWebTypesModule());

            var lifetimeScope = container.Build();
            var resolver = new AutofacDependencyResolver(lifetimeScope);

            DependencyResolver.SetResolver(resolver);
        }
    }
}
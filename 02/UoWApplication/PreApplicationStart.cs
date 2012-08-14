using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using UoWApplication;

/* this feature requires Integrate Pipeline and thus IIS. you cannot run this website using Casini.
 * i've configured the app to deploy to IIS with a virtual directory using the Web tab under Properties.
 * http://stackoverflow.com/questions/5087586/configuring-iis-windows-7-for-asp-net-asp-net-mvc-3
 * you'll also have to configure the app to run under a .NET 4.0 application pool or you'll get
 * a weird Web.config parse error for the <compilation debug="true" targetFramework="4.0" /> element
 * which will complain about your targetFramework version. NOTE: your app will likely be deployed
 * to a local IIS Express daemon. at least that was the case for me. so if you don't see your app show
 * up in inetmgr, then check your system tray for a blue icon. */
[assembly: PreApplicationStartMethod(typeof(PreApplicationStart), "Start")]

namespace UoWApplication
{
    public static class PreApplicationStart
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(SessionManagementHttpModule));
        }
    }
}
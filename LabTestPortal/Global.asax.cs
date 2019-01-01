using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace LabTestPortal {
    public class MvcApplication : System.Web.HttpApplication {
        public static WindsorContainer Container { get; set; }
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            BootstrapContainer();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void BootstrapContainer()
        {
            Container = new WindsorContainer();
            Container.Install(FromAssembly.This());
        }
    }
}

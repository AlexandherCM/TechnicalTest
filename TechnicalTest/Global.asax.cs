using Microsoft.Ajax.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TechnicalTest.InterfaceAdapter;

namespace TechnicalTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Configuración de la inyección de dependencias
            var builder = new ServiceCollection();

            //Configuración de tipo "Transient" ya que en NET framework no se cuenta con el ciclo de vida "Scoped"
            //y se desea que cada petición tenga su propia instancia.
            //Interfaces de la capa de aplicación ===============================================



            // ===================================================================================
            var serviceProvider = builder.BuildServiceProvider();
            DependencyResolver.SetResolver(new MyDependencyResolver(serviceProvider));
        }
    }
}

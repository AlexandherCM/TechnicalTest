using App.Interfaces;
using App.Services;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TechnicalTest.InterfaceAdapter;
using TechnicalTest.InterfaceAdapter.Adapters;

namespace TechnicalTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //Configuración de la inyección de dependencias
            var builder = new ServiceCollection();

            //Configuración de tipo "Transient" ya que en NET framework no se cuenta con el ciclo de vida "Scoped"
            //y se desea que cada petición tenga su propia instancia.

            //Interfaces de la capa de aplicación ===============================================
            builder.AddTransient<ISettings, ConfigSettingsAdapter>();
            builder.AddTransient<IHttp, HttpAdapter>();
            builder.AddTransient<ISerialize, SerializeAdapter>();

            //Interfaces de la capa de aplicación ===============================================
            builder.AddTransient<IPostalCodeService, PostalCodeService>();

            // ===================================================================================
            var serviceProvider = builder.BuildServiceProvider();

            //Configuración del resolver de dependencias para MVC   
            DependencyResolver.SetResolver(new MvcDependencyResolver(serviceProvider));

            //Configuración del resolver de dependencias para Web API
            GlobalConfiguration.Configure(config =>
            {
                WebApiConfig.Register(config);
                config.DependencyResolver = new WebApiDependencyResolver(serviceProvider);
            });

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

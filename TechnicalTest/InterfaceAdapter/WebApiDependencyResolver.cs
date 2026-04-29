using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;

namespace TechnicalTest.InterfaceAdapter
{
    public class WebApiDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _scope;

        public WebApiDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private WebApiDependencyResolver(IServiceScope scope)
        {
            _scope = scope;
            _serviceProvider = scope.ServiceProvider;
        }

        public object GetService(Type serviceType)
        {
            var service = _serviceProvider.GetService(serviceType);

            if (service != null)
            {
                return service;
            }

            if (typeof(IHttpController).IsAssignableFrom(serviceType))
            {
                return ActivatorUtilities.CreateInstance(_serviceProvider, serviceType);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            var scope = _serviceProvider.CreateScope();
            return new WebApiDependencyResolver(scope);
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TechnicalTest.InterfaceAdapter
{
    public class MvcDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public MvcDependencyResolver(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public object GetService(Type serviceType)
        {
            var service = _serviceProvider.GetService(serviceType);

            if (service != null)
            {
                return service;
            }

            if (typeof(IController).IsAssignableFrom(serviceType))
            {
                return ActivatorUtilities.CreateInstance(_serviceProvider, serviceType);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
            => _serviceProvider.GetServices(serviceType);
    }
}
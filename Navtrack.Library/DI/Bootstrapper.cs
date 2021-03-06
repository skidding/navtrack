using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Navtrack.Library.DI
{
    public static class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            IEnumerable<Assembly> assemblies = GetAssemblies();
            
            List<KeyValuePair<Type, ServiceAttribute>> serviceAttributes = assemblies
                .SelectMany(x => x.DefinedTypes)
                .SelectMany(x =>
                    x.GetCustomAttributes<ServiceAttribute>()
                        .Select(y => new KeyValuePair<Type, ServiceAttribute>(x, y)))
                .ToList();

            foreach (KeyValuePair<Type, ServiceAttribute> serviceAttribute in serviceAttributes)
            {
                serviceCollection.Add(new ServiceDescriptor(serviceAttribute.Value.Type, serviceAttribute.Key,
                    serviceAttribute.Value.ServiceLifetime));
            }
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            List<Assembly> assemblies = GetAssemblies(Assembly.GetEntryAssembly()).Distinct().ToList();

            return assemblies;
        }

        private static IEnumerable<Assembly> GetAssemblies(Assembly assembly)
        {
            foreach (Assembly referencedAssembly in assembly.GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith("Navtrack")).Select(Assembly.Load).SelectMany(GetAssemblies))
            {
                yield return referencedAssembly;
            }

            yield return assembly;
        }
    }
}
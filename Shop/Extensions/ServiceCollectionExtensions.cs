using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Scrutor;
using Microsoft.AspNetCore.NodeServices;

namespace Shop.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterDependencies(this IServiceCollection services)
		{
			Assembly dataAssembly = Assembly.Load(new AssemblyName("Data"));
			Assembly businessAssembly = Assembly.Load(new AssemblyName("Business"));

			services.Scan(scan => scan.FromAssemblies(dataAssembly)
				.AddClasses(classes => classes.Where(c => c.Name.EndsWith("Repository")))
				.AsImplementedInterfaces()
				.WithSingletonLifetime());

			services.Scan(scan => scan.FromAssemblies(dataAssembly)
				.AddClasses(classes => classes.Where(c => c.Name.EndsWith("Work")))
				.AsImplementedInterfaces()
				.WithSingletonLifetime());

			services.Scan(scan => scan.FromAssemblies(dataAssembly)
				.AddClasses(classes => classes.Where(c => c.Name.EndsWith("Context")))
				.AsImplementedInterfaces()
				.WithSingletonLifetime());

			services.Scan(scan => scan.FromAssemblies(businessAssembly)
				.AddClasses(classes => classes.Where(c => c.Name.EndsWith("Manager")))
				.AsImplementedInterfaces()
				.WithSingletonLifetime());

			return services;
		}
	}
}

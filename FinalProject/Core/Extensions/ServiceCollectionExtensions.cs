using System;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] coreModules)
		{
			foreach (var modul in coreModules)
			{
				modul.Load(serviceCollection);
			}
			return ServiceTool.Create(serviceCollection);
		}

    }
}


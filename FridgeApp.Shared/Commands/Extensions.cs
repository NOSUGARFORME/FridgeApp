using System.Reflection;
using FridgeApp.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeApp.Shared.Commands
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method for enabling <see cref="ICommand"/>.
        /// </summary>
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();
            
            services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
            services.Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            return services;
        }
    }
}
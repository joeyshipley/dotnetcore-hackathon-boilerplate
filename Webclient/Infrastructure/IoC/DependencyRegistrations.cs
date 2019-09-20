using System;
using System.Collections.Generic;
using System.Reflection;
using BOS.Webclient.Infrastructure.Db.ContextControl;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BOS.Webclient.Infrastructure.IoC
{
    public static class DependencyRegistrations
    {
        private static IServiceProvider _serviceProvider;
        private static readonly Assembly[] AutoResolvedAssemblies = new []
        {
            Assembly.Load("BOS.Webclient"),
        };

        public static void Register(IServiceCollection services)
        {
            RegisterStatelessDependencies(services);
            RegisterStatefulPerRequestDependencies(services);
            RegisterStatefulAcrossAllRequestsDependencies(services);
        }

        public static T Resolve<T>()
        {
            return Resolve<T>(new List<Action<IServiceCollection>>());
        }

        public static T Resolve<T>(List<Action<IServiceCollection>> registerResolverOverrides)
        {
            return (T) ServiceProvider(registerResolverOverrides).GetService(typeof(T));
        }

        private static IServiceProvider ServiceProvider(List<Action<IServiceCollection>> registerResolverOverrides)
        {
            var services = new ServiceCollection();

            RegisterServiceInjectableActionFilters(services);
            RegisterStatelessDependencies(services);
            RegisterStatefulPerRequestDependencies(services);
            RegisterStatefulAcrossAllRequestsDependencies(services);

            registerResolverOverrides.ForEach(register => register(services));

            _serviceProvider = _serviceProvider 
                ?? services.BuildServiceProvider();
            return _serviceProvider;
        }

        private static void RegisterServiceInjectableActionFilters(IServiceCollection services)
        {
            // services.AddScoped<ExampleActionFilter>();
        }

        private static void RegisterStatelessDependencies(IServiceCollection services)
        {
            services.Scan(sc => sc
                .FromCallingAssembly()
                .FromAssemblies(AutoResolvedAssemblies)
                .AddClasses()
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );
        }

        // INTENT: make stateful dependencies stand out

        private static void RegisterStatefulPerRequestDependencies(IServiceCollection services)
        {
            // services.AddScoped<>();
            services.AddScoped<IContextSessionProvider, ContextSessionProvider>();
        }

        private static void RegisterStatefulAcrossAllRequestsDependencies(IServiceCollection services)
        {
            // services.AddSingleton<>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
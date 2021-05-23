using System;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace SharedEditorBackend.Shared
{
    public static class StartupExtenstions
    {
        public static void InitSingleton<T>(this IApplicationBuilder app) => app.ApplicationServices.GetService<T>();
        public static void InitRadiator<T>(this IApplicationBuilder app) => app.InitSingleton<Radiator<T>>();
        public static void AddSubject<T>(this IServiceCollection services) => services.AddSingleton<Subject<T>>();
        public static void AddRadiator<T>(this IServiceCollection services, string endpoint) => services.AddSingleton<Radiator<T>>(GetRadiatorFactory<T>(endpoint));

        private static Func<IServiceProvider, Radiator<T>> GetRadiatorFactory<T>(string method) => (IServiceProvider provider) =>
        {
            var subject = provider.GetService<Subject<T>>();
            var context = provider.GetService<IHubContext<Trigger<T>>>();

            return new Radiator<T>(subject, context, method);
        };
    }
}
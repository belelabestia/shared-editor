using System;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedEditorBackend.Configuration;
using SharedEditorBackend.Features;
using SharedEditorBackend.Shared;

namespace SharedEditorBackend
{
    public class Startup
    {
        private AppSettings appSettings;
        public Startup(IConfiguration configuration) => appSettings = configuration.Get<AppSettings>();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddSingleton<Subject<Editor>>();
            services.AddSingleton<Subject<UserAction>>();

            services.AddSingleton<Radiator<UserAction>>(RadiatorFactory<UserAction>(appSettings.Endpoints.Frontend.UserAction));
            services.AddSingleton<Radiator<Editor>>(RadiatorFactory<Editor>(appSettings.Endpoints.Frontend.Editor));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<UserActionHub>(appSettings.Endpoints.Backend.SignalRPrefix + appSettings.Endpoints.Backend.UserAction);
                endpoints.MapHub<Hub<Editor>>(appSettings.Endpoints.Backend.SignalRPrefix + appSettings.Endpoints.Backend.Editor);
            });
        }

        private Func<IServiceProvider, Radiator<T>> RadiatorFactory<T>(string endpoint) => (IServiceProvider provider) =>
        {
            var subject = provider.GetService<Subject<T>>();
            var context = provider.GetService<IHubContext<Trigger<T>>>();

            return new Radiator<T>(subject, context, endpoint);
        };
    }
}

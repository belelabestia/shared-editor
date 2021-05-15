using System;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedEditorBackend.Features;
using SharedEditorBackend.Shared;

namespace SharedEditorBackend
{
    public class Startup
    {
        private Endpoints endpoints;
        public Startup(IConfiguration configuration) => this.endpoints = configuration.GetSection("Endpoints").Get<Endpoints>();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddSingleton<Subject<Editor>>();
            services.AddSingleton<Subject<UserAction>>();

            services.AddSingleton<Radiator<UserAction>>(RadiatorFactory<UserAction>(endpoints.Frontend.UserAction));
            services.AddSingleton<Radiator<Editor>>(RadiatorFactory<Editor>(endpoints.Frontend.Editor));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<UserActionHub>(this.endpoints.Backend.SignalRPrefix + this.endpoints.Backend.UserAction);
                endpoints.MapHub<Hub<Editor>>(this.endpoints.Backend.SignalRPrefix + this.endpoints.Backend.Editor);
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

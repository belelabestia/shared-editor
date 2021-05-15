using System;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.Builder;
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

            services.AddScoped<Trigger<UserAction>, UserActionTrigger>();
            
            services.AddSingleton<Radiator<UserAction>>(GetRadiatorFactory<UserAction>(appSettings.Endpoints.UserAction));
            services.AddSingleton<Radiator<Editor>>(GetRadiatorFactory<Editor>(appSettings.Endpoints.Editor));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Map(appSettings.Endpoints.Prefix, app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<Trigger<UserAction>>(appSettings.Endpoints.UserAction);
                    endpoints.MapHub<Trigger<Editor>>(appSettings.Endpoints.Editor);
                });
            });

            app.ApplicationServices.GetService<Radiator<UserAction>>().Activate();
            app.ApplicationServices.GetService<Radiator<Editor>>().Activate();
        }

        private Func<IServiceProvider, Radiator<T>> GetRadiatorFactory<T>(string method) => (IServiceProvider provider) =>
        {
            var subject = provider.GetService<Subject<T>>();
            var context = provider.GetService<IHubContext<Trigger<T>>>();

            return new Radiator<T>(subject, context, method);
        };
    }
}
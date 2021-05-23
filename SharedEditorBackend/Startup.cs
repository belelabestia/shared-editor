using Microsoft.AspNetCore.Builder;
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

            services.AddSubject<Editor>();
            services.AddSubject<UserAction>();

            services.AddRadiator<UserAction>(appSettings.Endpoints.UserAction);
            services.AddRadiator<Editor>(appSettings.Endpoints.Editor);

            services.AddScoped<Trigger<UserAction>, UserActionTrigger>();
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

            app.InitRadiator<UserAction>();
            app.InitRadiator<Editor>();
        }
    }
}
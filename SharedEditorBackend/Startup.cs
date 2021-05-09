using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedEditorBackend.Hubs;
using SharedEditorBackend.Services;

namespace SharedEditorBackend
{
    public class Startup
    {
        private IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR();
            services.AddSingleton<ConnectionService>();
            services.AddSingleton<UserActionNotificationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200");
                options.AllowCredentials();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ConnectionStateHub>(GetSignalRUrl("ConnectionState"));
            });
        }

        private string GetSignalRUrl(string endpointKey)
        {
            var signalrPrefix = configuration["Endpoints:SignalRPrefix"];
            var endpoint = configuration[$"Endpoints:{endpointKey}"];

            return signalrPrefix + endpoint;
        }
    }
}

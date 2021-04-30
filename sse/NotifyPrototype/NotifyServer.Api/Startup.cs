using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NotifyServer.Library.Hubs;
using NotifyServer.Library.Impl;
using NotifyServer.Library.Interface;
using NotifyServer.Library.Model.Entities;

namespace NotifyServer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000"));
            });

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });

            var release = Configuration["RELEASE"];

            if (string.IsNullOrEmpty(release))
            {
                release = "none";
            }

            var connectionString = Configuration.GetConnectionString("Database");

            services.AddDbContext<NotificationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions .EnableRetryOnFailure();
                });
            });

            services.AddScoped<INotificationSender, NotificationSender>();
            services.AddScoped<INotifyRepository, NotifyRepository>();
            services.AddScoped<INotifyApplication, NotifyApplication>();
            services.AddScoped<INotifyFormatter, NotifyJsonFormatter>();
        

            var applicationName = $"{Configuration["ApplicationName"]}({release})";

            AddSwagger(services, applicationName);

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Notifier API");
            });

            app.UseCors("CorsPolicy");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotifyHub>("/hubs/notifications", options =>
                {
                    options.Transports = HttpTransportType.ServerSentEvents | HttpTransportType.WebSockets | HttpTransportType.LongPolling;
                });
            });

            app.UseHttpsRedirection();
        }

        private void AddSwagger(IServiceCollection services, string applicationName)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"{applicationName} - {groupName}",
                    Version = groupName,
                    Description = "API Notification",
                    Contact = new OpenApiContact
                    {
                        Name = "BeyondNet",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
                    }
                });
            });
        }
    }
}

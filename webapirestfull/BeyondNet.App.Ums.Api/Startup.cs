using System;
using BeyondNet.App.Ums.Api.Bootstrapper.AutoMapperConfig;
using BeyondNet.App.Ums.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using NLog.Extensions.Logging;
using UMS.Infrastructure.Helpers.Paginations;
using UMS.Infrastructure.Helpers.Logs;
using BeyondNet.App.Ums.Helpers.Paginations;
using BeyondNet.App.Ums.DataAccess.EF;
using BeyondNet.App.Ums.DataAccess.EF.Users;
using BeyondNet.App.Ums.Domain.Common.Impl;
using BeyondNet.App.Ums.Domain.Common.Interface;
using BeyondNet.App.Ums.Domain.User;
using BeyondNet.App.Ums.Domain.User.Events;
using BeyondNet.App.Ums.Helpers.Binders;
using BeyondNet.App.Ums.Helpers.Cryptos;
using BeyondNet.App.Ums.Helpers.Hypermedias;
using BeyondNet.App.Ums.Helpers.Impl;
using BeyondNet.App.Ums.Helpers.Interface;
using BeyondNet.App.Ums.Helpers.Logging;

namespace UMS.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "UMS API", Version = "v1" });
            });

            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            }).AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                });


            //Pagination, PageList and Metadata
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                    .ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddScoped<IPaginationHelper, PaginationHelper>();

            //Repository Registrations
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<UmsDbContext>(options =>
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=beyondnet-ums;Trusted_Connection=True;ConnectRetryCount=0"));

            services.AddMvcCore();
            services.AddMvc();

            //Services 
            services.AddScoped<ILog, Log>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<IPasswordCryptographer, Md5PasswordCryptographer>();
            services.AddScoped<IHypermediaBuilder, HypermediaBuilder>();
      
            //User
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserApplicationService, UserApplicationService>();
            services.AddScoped<IHandles<UserCreatedEvent>, UserChangeEmailEventHandler>();
            services.AddScoped<IHandles<UserEmailChangedEvent>, UserChangeEmailEventHandler>();
            services.AddScoped<IPropertyMappingService, UserPropertyMappingService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddNLog();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global Exception Logger");

                            logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UMS API V1");
            });

            AutoMapperConfig.Initialize();

            app.UseMvc();

            DomainEvents.Init(serviceProvider);
        }
    }
}

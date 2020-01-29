using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Beauty.Barry.Api.Bootstrapper.Mapper;
using Beauty.Barry.Application;
using Common.Logging;
using Common.Logging.Configuration;
using Jal.Aop.LightInject;
using Jal.Aop.LightInject.Aspect.Installer;
using Jal.Aop.LightInject.Aspects.Correlation.Installer;
using Jal.Aop.LightInject.Aspects.Logger.Installer;
using Jal.Aop.LightInject.Aspects.Serializer.Installer;
using Jal.Aop.LightInject.Aspects.Serializer.Json.Installer;
using Jal.Bootstrapper.Impl;
using Jal.Bootstrapper.Interface;
using Jal.Bootstrapper.LightInject;
using Jal.Finder.Impl;
using Jal.Locator.LightInject.Installer;
using Jal.Settings.Configuration.LightInject.Installer;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Beauty.Barry.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = AssemblyFinder.Create(directory);

            var assemblies = finder.GetAssemblies();

            var compositionassembly = assemblies.Where(x => x.FullName.Contains("Beauty.Barry.Api")).ToArray();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Beauty.Barry.Api", Version = "v1" });
            });

            var applicationname = Configuration["ApplicationName"];

            var logging = Configuration.GetSection("LogConfiguration").Get<LogConfiguration>();

            LogManager.Configure(logging);

            var iocbootstrapper = new LightInjectBootStrapper(compositionassembly, serviceContainer =>
            {
                serviceContainer.RegisterSettings(Configuration);

                serviceContainer.RegisterFrom<ServiceLocatorCompositionRoot>();

                serviceContainer.RegisterAspects(new Assembly[] { });

                serviceContainer.RegisterFrom<AspectLoggerCompositionRoot>();

                serviceContainer.RegisterFrom<AspectSerializerCompositionRoot>();

                serviceContainer.RegisterFrom<AspectJsonSerializerCompositionRoot>();

                serviceContainer.RegisterFrom<AutomaticInterceptionCompositionRoot>();

                serviceContainer.RegisterSettings(Configuration);

                serviceContainer.RegisterFrom<AspectJsonSerializerCompositionRoot>();

                serviceContainer.RegisterCorrelation();

            }, new ContainerOptions { EnablePropertyInjection = false });

            var compositeBootstrapper = new CompositeBootstrapper(new IBootstrapper[] { iocbootstrapper });

            compositeBootstrapper.Configure();

            var container = iocbootstrapper.Result;

            var logger = LogManager.GetLogger(applicationname);

            container.Register(x => logger, new PerContainerLifetime());

            Mapper.Initialize(cfg => { cfg.AddProfiles(compositionassembly); });

            Mapper.Configuration.CompileMappings();

            //return container.CreateServiceProvider(services);

            var obj = container.CreateServiceProvider(services);

            var xyz = obj.GetService<IDepartmentApplication>();

            return obj;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Beauty.Barry.Api v1");
            });

         
        }
    }
}

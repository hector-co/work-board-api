using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Qurl.AspNetCore;
using WorkBoard.Api.Filters;
using WorkBoard.Api.Middlewares;
using WorkBoard.IocConfig;
using System;

namespace WorkBoard.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Enviroment = env;
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Enviroment { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.Filters.Add<ValidationActionFilter>();
                opt.Filters.Add<TransactionActionFilter>();
            })
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddFluentValidation();

            services.AddQurlModelBinder();

            services.AddCors();

            ApplicationContainer = services.GetConfiguredContainer(Configuration, Enviroment);

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //TODO: verify for production
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            }
            else if (env.IsStaging())
            {
                app.UseCors(options => options.WithOrigins(Configuration["AllowedHosts"]).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<RequestLoggerMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseMvc();
        }
    }
}

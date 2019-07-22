using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hco.Base.DataAccess.Ef;
using Hco.Base.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WorkBoard.Application.Exceptions;
using WorkBoard.DataAccess.Ef;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace WorkBoard.IocConfig
{
    public static class ContainerConfigurator
    {
        public static IContainer GetConfiguredContainer(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ContainerBuilder();

            services.AddDbContext<WorkBoardContext>(options => options.UseSqlServer(configuration.GetConnectionString("WorkBoard")));

            builder.Populate(services);

            Configure(builder, configuration, env);

            var container = builder.Build();
            var dbContext = container.Resolve<WorkBoardContext>();
            dbContext.Database.Migrate();

            return container;
        }

        public static void Configure(ContainerBuilder containerBuilder, IConfiguration configuration, IHostingEnvironment env)
        {
            ConfigureMediatR(containerBuilder);

            containerBuilder.RegisterType<UnitOfWorkEf<WorkBoardContext>>().AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.RegisterTypes(typeof(WorkBoardContext).GetTypeInfo().Assembly.GetTypes()).AsImplementedInterfaces();
            containerBuilder.RegisterTypes(typeof(CommandException).GetTypeInfo().Assembly.GetTypes()).AsImplementedInterfaces();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            containerBuilder.RegisterInstance(Log.Logger).AsImplementedInterfaces();

            containerBuilder.RegisterType<FakeEventStore>().AsImplementedInterfaces();

            //TODO: register Domain.Services
        }

        private static void ConfigureMediatR(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            containerBuilder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }

    public class FakeEventStore : IEventStore
    {
        public IEnumerable<Event> ListEventsForAggregate(Guid aggregateGuid)
        {
            return new List<Event> { };
        }

        public IEnumerable<Event> ListEventsForAggregate(Guid aggregateGuid, int maxVersion)
        {
            return new List<Event> { };
        }

        public IEnumerable<Event> ListEventsForAggregate(Guid aggregateGuid, DateTime maxDateTime)
        {
            return new List<Event> { };
        }

        public void Save<TEvent>(IEnumerable<TEvent> events, int expectedVersion) where TEvent : Event
        {
        }
    }
}

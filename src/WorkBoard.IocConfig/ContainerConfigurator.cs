using Autofac;
using Hco.Base.DataAccess.Ef;
using Hco.Base.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using WorkBoard.Commands.Exceptions;
using WorkBoard.DataAccess.Dapper.UserDataAccess.Queries;
using WorkBoard.DataAccess.Ef;

namespace WorkBoard.IocConfig
{
    public static class ContainerConfigurator
    {
        public static void RegisterContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkBoardContext>(options => options.UseSqlServer(configuration.GetConnectionString("WorkBoard")));
        }

        public static void Configure(this ContainerBuilder builder, IConfiguration configuration)
        {
            ConfigureMediatR(builder);

            builder.RegisterType<UnitOfWorkEf<WorkBoardContext>>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterTypes(typeof(WorkBoardContext).GetTypeInfo().Assembly.GetTypes()).AsImplementedInterfaces();
            builder.RegisterTypes(typeof(CommandException).GetTypeInfo().Assembly.GetTypes()).AsImplementedInterfaces();
            builder.RegisterTypes(typeof(UserDtoGetByIdQueryHandler).GetTypeInfo().Assembly.GetTypes()).AsImplementedInterfaces();

            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();

            builder.RegisterInstance(Log.Logger).AsImplementedInterfaces();

            builder.RegisterType<FakeEventStore>().AsImplementedInterfaces();

            //TODO: register Domain.Services
        }

        public static void InitContext(this ILifetimeScope container)
        {
            var dbContext = container.Resolve<WorkBoardContext>();
            dbContext.Database.Migrate();

            DbInitializer.AddSampleData(dbContext);
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

using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Domain;
using FamilyRegistration.Core.Pipeline;
using FamilyRegistration.Core.Pipeline.Middlewares;
using FamilyRegistration.Core.Strategy;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;
using FamilyRegistration.Data.Queue.BackgroundServices;
using FamilyRegistration.Data.Queue.Common;
using FamilyRegistration.EFCore;
using FamilyRegistration.Patterns.Observer;
using FamilyRegistration.Patterns.Pipeline;
using FamilyRegistration.Web.Application;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

namespace FamilyRegistration.Web.Config;

public static class ConfigExtensions
{
    public static void ConfigureCustomSettings(this IServiceCollection services, IConfiguration configuration)
    {
        //using (IServiceScope scope = _serviceProvider.CreateScope())
        //{
        //    IScopedProcessingService scopedProcessingService =
        //        scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

        //    await scopedProcessingService.DoWorkAsync(stoppingToken);
        //}

        services.AddDbContextFactory<FamilyDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresDataSource"));
        }, ServiceLifetime.Singleton);

        services.AddOptions<CustomSettings>()
            .Bind(configuration.GetSection(CustomSettings.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        CustomSettings? customSettings = configuration
            .GetSection(CustomSettings.SectionName)
            .Get<CustomSettings>();

        if (customSettings == null) return;

        if (customSettings.Strategy == EStrategy.Pipeline)
        {
            services
                .AddSingleton<IProcessDataStrategy, ProcessDataWithPipeline>()
                .AddSingleton<Pipeline<FamilyContext>, ScoreCalculatorPipeline>()
                .AddSingleton<IMiddleware<FamilyContext>, FamilyIncomeScoreMiddleware>()
                .AddSingleton<IMiddleware<FamilyContext>, NumOfDependentsMiddleware>();
        }
        else if (customSettings.Strategy == EStrategy.Decorator)
        {
            services
                .AddSingleton<IProcessDataStrategy, ProcessDataWithDecorator>()
                .AddSingleton<AbstractScoreCalculator, AggregateScoreCalculator>();
        }
        else if (customSettings.Strategy == EStrategy.Observer)
        {
            services
                .AddSingleton<IProcessDataStrategy, ProcessDataWithObservers>();
        }
        else if (customSettings.Strategy == EStrategy.Composite)
        {
            services
                .AddSingleton<IProcessDataStrategy, ProcessDataWithComposite>();
        }
        else
        {
            services
                .AddSingleton<IProcessDataStrategy, ProcessDataWithTransactionScript>();
        }

        services
            .AddSingleton<IDataSource, SampleDataGenerator>()
            .AddSingleton<IProcessDataUseCase, ProcessDataUseCase>();

        services.AddOptions<AmqpSettings>()
           .Bind(configuration.GetSection(AmqpSettings.SectionName))
           .ValidateDataAnnotations()
           .ValidateOnStart();

        AmqpSettings? amqpSettings = configuration
            .GetSection(AmqpSettings.SectionName)
            .Get<AmqpSettings>();

        if (amqpSettings != null && amqpSettings.Enabled == true)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = amqpSettings.HostName,
                UserName = amqpSettings.UserName,
                Password = amqpSettings.Password,
                VirtualHost = amqpSettings.VirtualHost
            };

            services.AddSingleton(connectionFactory);

            //configure and register observable handlers for decoupling background services            
            services
                .AddSingleton<IRabbitMqProducer<ProcessDataOutput>, ProcessDataOutputProducer>()
                .AddSingleton<IObserverOf<ProcessDataInput>, ProcessDataInputHandler>()
                .AddSingleton<IObserverOf<ProcessDataOutput>, ProcessDataOutputHandler>()
                .AddSingleton<IObservableOf<ProcessDataInput>, ProcessDataInputObservable>()
                .AddSingleton<IObservableOf<ProcessDataOutput>, ProcessDataOutputObservable>()
                .AddHostedService<ConsumeFamilyInput>();

        }
    }
}

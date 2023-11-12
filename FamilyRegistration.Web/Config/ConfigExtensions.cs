using FamilyRegistration.Core;
using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Pipeline;
using FamilyRegistration.Core.Pipeline.Middlewares;
using FamilyRegistration.Core.Strategy;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;
using FamilyRegistration.Data.Queue.BackgroundServices;
using FamilyRegistration.Data.Queue.Common;
using FamilyRegistration.Patterns.Observer;
using FamilyRegistration.Patterns.Pipeline;
using FamilyRegistration.Web.Application;
using RabbitMQ.Client;

namespace FamilyRegistration.Web.Config;

public static class ConfigExtensions
{
    public static void ConfigureCustomSettings(this IServiceCollection services, IConfiguration configuration)
    {
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
            services.AddSingleton<IProcessDataStrategy, ProcessDataWithObservers>();
        }
        else if (customSettings.Strategy == EStrategy.Composite)
        {
            services.AddSingleton<IProcessDataStrategy, ProcessDataWithComposite>();
        }
        else
        {
            services.AddSingleton<IProcessDataStrategy, ProcessDataWithTransactionScript>();
        }

        services.AddSingleton<IDataSource, SampleDataGenerator>();
        services.AddSingleton<IProcessDataUseCase, ProcessDataUseCase>();

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
            services.AddSingleton<IRabbitMqProducer<ProcessDataOutput>, ProcessDataOutputProducer>();
            services.AddSingleton<IObserverOf<ProcessDataInput>, ProcessDataInputHandler>();
            services.AddSingleton<IObserverOf<ProcessDataOutput>, ProcessDataOutputHandler>();
            services.AddSingleton<IObservableOf<ProcessDataInput>, ProcessDataInputObservable>();
            services.AddSingleton<IObservableOf<ProcessDataOutput>, ProcessDataOutputObservable>();

            services.AddHostedService<ConsumeFamilyInput>();

        }
    }
}

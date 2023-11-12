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
        services.AddOptions<AmqpSettings>()
            .Bind(configuration.GetSection(AmqpSettings.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IDataSource, SampleDataGenerator>();
        services.AddScoped<IProcessDataUseCase, ProcessDataUseCase>();

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

            services.AddSingleton<ConnectionFactory>(connectionFactory);

            //configure and register observable handlers for decoupling background services
            var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                .SetMinimumLevel(LogLevel.Trace)
                .AddConsole());

            ILogger<RabbitMqClientBase> logger = loggerFactory.CreateLogger<RabbitMqClientBase>();

            var producer = new ProcessDataOutputProducer(connectionFactory, loggerFactory);

            var processDataInputPublisher = new GenericSubject<ProcessDataInput>();
            var processDataOutputPublisher = new GenericSubject<ProcessDataOutput>();
            processDataInputPublisher.Register(new ProcessDataInputHandler(processDataOutputPublisher));
            processDataOutputPublisher.Register(new ProcessDataOutputHandler(producer));

            services.AddSingleton<ISubject<ProcessDataInput>>(processDataInputPublisher);
            services.AddSingleton<ISubject<ProcessDataOutput>>(processDataOutputPublisher);
            services.AddHostedService<ConsumeFamilyInput>();
        }

        CustomSettings? customSettings = configuration
            .GetSection(CustomSettings.SectionName)
            .Get<CustomSettings>();

        if (customSettings == null) return;

        if (customSettings.Strategy == EStrategy.Pipeline)
        {
            services
                .AddScoped<IProcessDataStrategy, ProcessDataWithPipeline>()
                .AddScoped<Pipeline<FamilyContext>, ScoreCalculatorPipeline>()
                .AddScoped<IMiddleware<FamilyContext>, FamilyIncomeScoreMiddleware>()
                .AddScoped<IMiddleware<FamilyContext>, NumOfDependentsMiddleware>();
        }
        else if (customSettings.Strategy == EStrategy.Decorator)
        {
            services
                .AddScoped<IProcessDataStrategy, ProcessDataWithDecorator>()
                .AddScoped<AbstractScoreCalculator, AggregateScoreCalculator>();
        }
        else if (customSettings.Strategy == EStrategy.Observer)
        {
            services.AddScoped<IProcessDataStrategy, ProcessDataWithObservers>();
        }
        else if (customSettings.Strategy == EStrategy.Composite)
        {
            services.AddScoped<IProcessDataStrategy, ProcessDataWithComposite>();
        }
        else
        {
            services.AddScoped<IProcessDataStrategy, ProcessDataWithTransactionScript>();
        }
    }
}

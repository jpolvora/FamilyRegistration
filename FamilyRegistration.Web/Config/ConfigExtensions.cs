using FamilyRegistration.Core;
using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Pipeline;
using FamilyRegistration.Core.Pipeline.Middlewares;
using FamilyRegistration.Core.Strategy;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;
using FamilyRegistration.Data.Queue;
using FamilyRegistration.Patterns.Pipeline;

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

        AmqpSettings? amqpSettings = configuration.GetSection(AmqpSettings.SectionName).Get<AmqpSettings>();
        if (amqpSettings != null && amqpSettings.Enabled == true)
        {
            services.AddSingleton<AmqpSettings>(amqpSettings);
            services.AddHostedService<ConsumeRabbitMQHostedService>();
        }

        CustomSettings? customSettings = configuration.GetSection(CustomSettings.SectionName).Get<CustomSettings>();

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

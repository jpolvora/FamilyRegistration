using FamilyRegistration.Core;
using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Pipelines;
using FamilyRegistration.Core.Strategy;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;
using FamilyRegistration.Web.Routes;
using MiddlewarePipelineLib;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IDataSource, SampleDataGenerator>();

        string? processarListaStrategy = builder.Configuration.GetValue<string>("CustomSettings:Strategy");

        if (processarListaStrategy == "Pipeline")
        {
            builder.Services.AddScoped<IProcessDataStrategy, ProcessDataWithPipeline>();
            builder.Services.AddScoped<MiddlewarePipeline<FamilyRegistrationContext>, CustomPipeline>();
        }
        else if (processarListaStrategy == "Decorator")
        {
            builder.Services.AddScoped<IProcessDataStrategy, ProcessDataWithDecorator>();
            builder.Services.AddScoped<AbstractScoreCalculator, AggregateScoreCalculator>();
        }
        else
        {
            //no design pattern !
            builder.Services.AddScoped<IProcessDataStrategy, ProcessDataWithTransactionScript>();
        }

        builder.Services.AddScoped<IProcessDataUseCase, ProcessDataUseCase>();

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.MapGet("/", RouteHandlers.HandleGet)
            .WithName("GetSampleData")
            .WithOpenApi();

        app.MapPost("/", RouteHandlers.HandlePost)
            .WithName("PostSampleData")
            .WithOpenApi();

        app.MapPost("/json", RouteHandlers.HandleJsonPost)
            .WithName("PostJsonFormatOne")
            .WithOpenApi();

        app.MapGet("/pipeline", RouteHandlers.HandleGetWithPipelineStrategy)
            .WithName("GetSampleDataPipeline")
            .WithOpenApi();

        app.MapGet("/decorator", RouteHandlers.HandleGetWithDecoratorStrattegy)
            .WithName("GetSampleDataDecorator")
            .WithOpenApi();

        app.Run();
    }
}
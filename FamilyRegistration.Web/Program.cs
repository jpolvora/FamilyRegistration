using FamilyRegistration.Core;
using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Pipelines;
using FamilyRegistration.Core.UseCases.ProcessarLista;
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

        builder.Services.AddScoped<MiddlewarePipeline<FamilyRegistrationContext>, CustomPipeline>();
        builder.Services.AddScoped<ScoreCalculator, AggregateScoreCalculator>();
        builder.Services.AddScoped<IProcessarListaUseCase, ProcessarListaPipeline>();
        //builder.Services.AddScoped<IProcessarListaUseCase, ProcessarListaDecorator>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGet("/", RouteHandlers.HandleGet)
            .WithName("GetSampleData")
            .WithOpenApi();

        app.MapPost("/", RouteHandlers.HandlePost)
            .WithName("PostSampleData")
            .WithOpenApi();


        app.Run();
    }
}
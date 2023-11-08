using FamilyRegistration.Core;
using FamilyRegistration.Core.Pipelines;
using FamilyRegistration.Core.UseCases;
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
        builder.Services.AddScoped<IProcessarListaStrategyUseCase, ProcessarListaStrategy>();
        builder.Services.AddScoped<IProcessarListaUseCase, ProcessarListaPipelineUseCase>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGet("/pipeline", PipelineRouteHandlers.HandleGet)
            .WithName("GetSampleData_Pipeline")
            .WithOpenApi();

        app.MapPost("/pipeline", PipelineRouteHandlers.HandlePost)
            .WithName("PostSampleData_Pipeline")
            .WithOpenApi();

        app.MapGet("/decorator", DecoratorRouteHandlers.HandleGet)
         .WithName("GetSampleData_Decorator")
         .WithOpenApi();

        app.MapPost("/decorator", DecoratorRouteHandlers.HandlePost)
            .WithName("PostSampleData_Decorator")
            .WithOpenApi();

        app.Run();
    }
}
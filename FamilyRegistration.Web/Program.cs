using FamilyRegistration.Core.Middlewares;
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
        builder.Services.AddScoped<IProcessarListaUseCase, ProcessarListaUseCase>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGet("/", SampleDataRoute.Handle)
            .WithName("GetSampleData")
            .WithOpenApi();

        app.MapPost("/", SampleDataRoute.HandlePost)
            .WithName("PostSampleData")
            .WithOpenApi();


        app.Run();
    }
}
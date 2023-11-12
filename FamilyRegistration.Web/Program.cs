using FamilyRegistration.Web.Config;
using FamilyRegistration.Web.Routes;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var (services, configuration) = builder;

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //custom config class
        builder.Services.ConfigureCustomSettings(builder.Configuration);

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseMiddleware<ResponseTimeMiddleware>();

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


        app.MapGet("/transaction", RouteHandlers.HandleGetWithTransactionScriptStrategy)
            .WithName("GetSampleDataTransaction")
            .WithOpenApi();

        app.MapGet("/observer", RouteHandlers.HandleGetWithObserverStrategy)
            .WithName("GetSampleDataObserver")
            .WithOpenApi();

        app.MapGet("/composite", RouteHandlers.HandleGetWithObserverComposite)
            .WithName("GetSampleDataComposite")
            .WithOpenApi();

        app.Run();
    }
}

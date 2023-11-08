using FamilyRegistration.Core;
using FamilyRegistration.Core.UseCases;
using FamilyRegistration.Web.Routes;
using MiddlewarePipelineLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MiddlewarePipeline<FamilyContext>, CustomPipeline>();
builder.Services.AddScoped<IUseCase<ProcessarListaInput, ProcessarListaOutput>, ProcessarLista>();

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

app.Run();
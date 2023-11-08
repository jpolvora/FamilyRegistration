using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace FamilyRegistration.Integration.Tests;

public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

            //services.AddScoped<MiddlewarePipeline<FamilyRegistrationContext>, CustomPipeline>();
            //services.AddScoped<IProcessarListaUseCase, ProcessarListaUseCase>();

            //var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TodoGroupDbContext>));

            //if (descriptor != null)
            //{
            //    services.Remove(descriptor);
            //}

            //services.AddDbContext<TodoGroupDbContext>(options =>
            //{
            //    var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //    options.UseSqlite($"Data Source={Path.Join(path, "WebMinRouteGroup_tests.db")}");
            //});
        });

        return base.CreateHost(builder);
    }
}
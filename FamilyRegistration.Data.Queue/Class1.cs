using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FamilyRegistration.Data.Queue;

public class AmqpService : IDisposable
{
    private readonly IConfiguration _configuration;

    public AmqpService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
    }
}


public class Worker : BackgroundService
{
    private readonly AmqpService _amqpService;

    public Worker(IConfiguration configuration)
    {
        _amqpService = new AmqpService(configuration);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        base.Dispose();
        _amqpService.Dispose();
    }
}

using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data.Queue.Common;
using FamilyRegistration.EFCore;
using FamilyRegistration.Patterns.Observer;
using Microsoft.EntityFrameworkCore;

namespace FamilyRegistration.Web.Application;

public class ProcessDataOutputHandler : GenericObserverOf<ProcessDataOutput>
{
    private readonly IRabbitMqProducer<ProcessDataOutput> _producer;
    private readonly IDbContextFactory<FamilyDbContext> _dbContextFactory;

    public ProcessDataOutputHandler(IRabbitMqProducer<ProcessDataOutput> producer, IDbContextFactory<FamilyDbContext> dbContextFactory)
    {
        _producer = producer;
        _dbContextFactory = dbContextFactory;
    }

    public override async Task HandleNotification(ProcessDataOutput value)
    {
        using (var db = this._dbContextFactory.CreateDbContext())
        {

            ////publish to queue
            foreach (var item in value)
            {
                //save to database
                var pessoa = new Pessoa()
                {
                    Id = Guid.Parse(item.Key),
                };
                db.Pessoas.Add(pessoa);
            }

            await db.SaveChangesAsync();
        }

        //publish to queue
        _producer.Publish(value);
    }
}

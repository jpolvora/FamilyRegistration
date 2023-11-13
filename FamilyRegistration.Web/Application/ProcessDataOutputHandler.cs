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
            foreach (var item in value)
            {
                //save or update                
                if (Guid.TryParse(item.Key, out Guid key))
                {
                    var pessoa = await db.Pessoas.FirstOrDefaultAsync(p => p.Id == key);
                    if (pessoa is not null)
                    {
                        //update                                                
                        pessoa.Dependents = item.NumOfDependents;
                        pessoa.Income = item.FamilyIncome;
                        pessoa.FullName = "Nome do responsável da família";
                        pessoa.Gender = "Male";
                        pessoa.Score = item.Score;
                    }
                    else
                    {
                        pessoa = new Pessoa()
                        {
                            Id = key,
                            Age = 0,
                            Dependents = item.NumOfDependents,
                            Income = item.FamilyIncome,
                            FullName = "Nome do responsável da família",
                            Gender = "Male",
                            Score = item.Score
                        };

                        db.Pessoas.Add(pessoa);
                    }
                }
            }

            await db.SaveChangesAsync();
        }

        //publish to queue
        _producer.Publish(value);
    }
}

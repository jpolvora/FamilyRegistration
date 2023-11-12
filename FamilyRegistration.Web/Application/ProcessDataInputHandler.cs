using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Strategy;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Web.Application;
public class ProcessDataInputHandler : GenericObserver<ProcessDataInput>
{
    private readonly ISubject<ProcessDataOutput> _publisher;

    public ProcessDataInputHandler(ISubject<ProcessDataOutput> publisher)
    {
        _publisher = publisher;
    }
    public override async Task Update(ProcessDataInput value)
    {
        //throw new NotImplementedException();
        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline
        IProcessDataStrategy strategy = new ProcessDataWithDecorator(new AggregateScoreCalculator());
        IProcessDataUseCase useCase = new ProcessDataUseCase(strategy);
        var output = await useCase.Execute(value);
        //ordenar o output pelo Score mais alto
        var result = new ProcessDataOutput(output.OrderByDescending(x => x.Score));

        await this._publisher.Publish(result);

    }
}

using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Web.Application;
public class ProcessDataInputHandler : GenericObserverOf<ProcessDataInput>
{
    private readonly IObservableOf<ProcessDataOutput> _eventPublisher;
    private readonly IProcessDataUseCase _useCase;

    public ProcessDataInputHandler(IObservableOf<ProcessDataOutput> publisher,
        IProcessDataUseCase useCase)
    {
        _eventPublisher = publisher;
        //_useCase = ProcessData.WithPipeline();
        _useCase = useCase;
    }
    public override async Task HandleNotification(ProcessDataInput value)
    {
        var output = await _useCase.Execute(value);
        //ordenar o output pelo Score mais alto
        var result = new ProcessDataOutput(output.OrderByDescending(x => x.Score));

        await this._eventPublisher.Notify(result);

    }
}
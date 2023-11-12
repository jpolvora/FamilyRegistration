using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Web.Application;

public class ProcessDataInputObservable : GenericObservableOf<ProcessDataInput>
{
    public ProcessDataInputObservable(IEnumerable<IObserverOf<ProcessDataInput>> observers)
    {
        foreach (var observer in observers)
        {
            this.Register(observer);
        }
    }
}

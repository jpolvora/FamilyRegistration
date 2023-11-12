using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Web.Application;

public class ProcessDataOutputObservable : GenericObservableOf<ProcessDataOutput>
{
    public ProcessDataOutputObservable(IEnumerable<IObserverOf<ProcessDataOutput>> observers)
    {
        foreach (var observer in observers)
        {
            this.Register(observer);
        }
    }
}

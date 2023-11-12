using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Web.Application;

public class ProcessDataOutputPublisher : GenericSubject<ProcessDataOutput>
{
    public ProcessDataOutputPublisher(IEnumerable<Patterns.Observer.IObserver<ProcessDataOutput>> observers)
    {
        foreach (var observer in observers)
        {
            this.Register(observer);
        }
    }
}

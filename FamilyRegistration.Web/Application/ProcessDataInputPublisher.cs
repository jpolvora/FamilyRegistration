using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Web.Application;

public class ProcessDataInputPublisher : GenericSubject<ProcessDataInput>
{
    public ProcessDataInputPublisher(IEnumerable<Patterns.Observer.IObserver<ProcessDataInput>> observers)
    {
        foreach (var observer in observers)
        {
            this.Register(observer);
        }
    }
}

namespace FamilyRegistration.Core.Observer;

// Concrete observer
public abstract class AbstractObserver : IObserver<FamilyRegistrationContext>
{

    public AbstractObserver(ISubject<FamilyRegistrationContext> subject)
    {
        subject.Register(this);
    }

    public AbstractObserver()
    {

    }

    public abstract Task Update(FamilyRegistrationContext? value);
}
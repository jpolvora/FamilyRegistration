namespace FamilyRegistration.Core.Observer;

// Concrete observer
public abstract class AbstractObserver : IObserver<FamilyRegistrationContext>
{

    public AbstractObserver(ISubject<FamilyRegistrationContext> subject)
    {
        subject.Attach(this);
    }

    public AbstractObserver()
    {

    }


    public abstract void Update(FamilyRegistrationContext? value);
}
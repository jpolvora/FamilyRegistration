namespace FamilyRegistration.Core.Observer;

public abstract class FamilyRegistrationContextObserver : IObserver<FamilyContext>
{
    public abstract Task Update(FamilyContext value);
}
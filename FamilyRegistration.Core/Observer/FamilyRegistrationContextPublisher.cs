namespace FamilyRegistration.Core.Observer;

public class FamilyRegistrationContextPublisher : ISubject<FamilyRegistrationContext>
{
    private readonly List<IObserver<FamilyRegistrationContext>> _observers = new();

    public async Task Publish(FamilyRegistrationContext value)
    {
        foreach (var observer in _observers)
        {
            await observer.Update(value);
        }
    }

    public void Register(IObserver<FamilyRegistrationContext> observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver<FamilyRegistrationContext> observer)
    {
        _observers.Remove(observer);
    }

    public void Dispose() => this._observers.Clear();
}

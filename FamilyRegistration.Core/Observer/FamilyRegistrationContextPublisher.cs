namespace FamilyRegistration.Core.Observer;

public class FamilyRegistrationContextPublisher : ISubject<FamilyContext>
{
    private readonly List<IObserver<FamilyContext>> _observers = new();

    public async Task Publish(FamilyContext value)
    {
        foreach (var observer in _observers)
        {
            await observer.Update(value);
        }
    }

    public void Register(IObserver<FamilyContext> observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver<FamilyContext> observer)
    {
        _observers.Remove(observer);
    }

    public void Dispose() => this._observers.Clear();
}

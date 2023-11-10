namespace FamilyRegistration.Core.Observer;

// Concrete subject
public class ConcreteSubject : ISubject<FamilyRegistrationContext>
{
    private readonly List<IObserver<FamilyRegistrationContext>> _observers = new();
    private FamilyRegistrationContext? _state;

    public async Task SetState(FamilyRegistrationContext value)
    {
        _state = value;
        await Notify();
    }

    public void Attach(IObserver<FamilyRegistrationContext> observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver<FamilyRegistrationContext> observer)
    {
        _observers.Remove(observer);
    }

    //public void Dispose()
    //{
    //    foreach (var observer in _observers)
    //    {
    //        this.Detach(observer);
    //    }
    //}

    public async Task Notify()
    {
        foreach (var observer in _observers)
        {
            await observer.Update(_state);
        }
    }

    public void Dispose()
    {
        this._observers.Clear();
    }
}

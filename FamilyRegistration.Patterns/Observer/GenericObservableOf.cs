namespace FamilyRegistration.Patterns.Observer;

public class GenericObservableOf<TContext> : IObservableOf<TContext>
{
    private readonly List<IObserverOf<TContext>> _observers = new();

    public async Task Notify(TContext value)
    {
        foreach (var observer in _observers)
        {
            await observer.HandleNotification(value);
        }
    }

    public void Register(IObserverOf<TContext> observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserverOf<TContext> observer)
    {
        _observers.Remove(observer);
    }

    public void Dispose() => this._observers.Clear();
}

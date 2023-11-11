namespace FamilyRegistration.Patterns.Observer;

public class GenericSubject<TContext> : ISubject<TContext>
{
    private readonly List<IObserver<TContext>> _observers = new();

    public async Task Publish(TContext value)
    {
        foreach (var observer in _observers)
        {
            await observer.Update(value);
        }
    }

    public void Register(IObserver<TContext> observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver<TContext> observer)
    {
        _observers.Remove(observer);
    }

    public void Dispose() => this._observers.Clear();
}

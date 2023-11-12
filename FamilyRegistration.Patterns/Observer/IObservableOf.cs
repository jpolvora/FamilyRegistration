namespace FamilyRegistration.Patterns.Observer;

// Subject interface
public interface IObservableOf<TContext> : IDisposable
{
    void Register(IObserverOf<TContext> observer);
    void Detach(IObserverOf<TContext> observer);
    Task Notify(TContext value);
}

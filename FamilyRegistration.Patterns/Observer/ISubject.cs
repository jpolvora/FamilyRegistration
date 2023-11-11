namespace FamilyRegistration.Patterns.Observer;

// Subject interface
public interface ISubject<TContext> : IDisposable
{
    void Register(IObserver<TContext> observer);
    void Detach(IObserver<TContext> observer);
    Task Publish(TContext value);
}

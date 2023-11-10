namespace FamilyRegistration.Core.Observer;

// Subject interface
public interface ISubject<TContext> : IDisposable
{
    void Attach(IObserver<TContext> observer);
    void Detach(IObserver<TContext> observer);
    void Notify();
}

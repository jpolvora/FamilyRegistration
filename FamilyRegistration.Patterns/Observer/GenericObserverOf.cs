namespace FamilyRegistration.Patterns.Observer;

public abstract class GenericObserverOf<TContext> : IObserverOf<TContext>
{
    public abstract Task HandleNotification(TContext value);
}

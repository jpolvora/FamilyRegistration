namespace FamilyRegistration.Patterns.Observer;

// Observer interface
public interface IObserverOf<TContext>
{
    Task HandleNotification(TContext context);
}

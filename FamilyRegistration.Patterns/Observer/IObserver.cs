namespace FamilyRegistration.Patterns.Observer;

// Observer interface
public interface IObserver<TContext>
{
    Task Update(TContext context);
}

namespace FamilyRegistration.Core.Observer;

// Observer interface
public interface IObserver<TContext>
{
    Task Update(TContext context);
}

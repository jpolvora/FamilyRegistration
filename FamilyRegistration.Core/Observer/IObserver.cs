namespace FamilyRegistration.Core.Observer;

// Observer interface
public interface IObserver<TContext>
{
    void Update(TContext? context);
}

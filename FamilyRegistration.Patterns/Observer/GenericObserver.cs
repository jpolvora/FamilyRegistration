namespace FamilyRegistration.Patterns.Observer;

public abstract class GenericObserver<TContext> : IObserver<TContext>
{
    public abstract Task Update(TContext value);
}

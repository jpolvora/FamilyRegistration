namespace FamilyRegistration.Patterns.Composite;

public abstract class AbstractComponent<TContext>
{
    public abstract Task Execute(TContext context);
}

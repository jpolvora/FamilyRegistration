namespace FamilyRegistration.Patterns.Composite;

public interface IComposite<TContext>
{
    void Add(AbstractComponent<TContext> component);
    void Remove(AbstractComponent<TContext> component);
}

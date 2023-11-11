namespace FamilyRegistration.Patterns.Composite;

public abstract class GenericCompositeComponent<TContext> : AbstractComponent<TContext>, IComposite<TContext>
{
    private readonly List<AbstractComponent<TContext>> _components;

    public GenericCompositeComponent()
    {
        this._components = new List<AbstractComponent<TContext>>();
    }
    public override async Task Execute(TContext context)
    {
        foreach (var child in _components)
        {
            await child.Execute(context);
        }
    }

    public void Add(AbstractComponent<TContext> component)
    {
        this._components.Add(component);
    }

    public void Remove(AbstractComponent<TContext> component)
    {
        this._components.Remove(component);
    }
}

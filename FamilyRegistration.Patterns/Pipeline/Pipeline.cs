namespace FamilyRegistration.Patterns.Pipeline;

public class Pipeline<TContext>
{
    private readonly List<IMiddleware<TContext>> _middlewares = new();

    public void Use(IMiddleware<TContext> middleware)
    {
        _middlewares.Add(middleware);
    }

    public int Count => _middlewares.Count;

    public async Task Execute(TContext context)
    {
        foreach (var middleware in _middlewares)
        {
            await middleware.Execute(context);
        }
    }
}
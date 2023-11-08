namespace MiddlewarePipelineLib;

public class MiddlewarePipeline<TContext> where TContext : MiddlewareContext
{
    private readonly List<IMiddleware<TContext>> _middlewares = new();

    public void Use(IMiddleware<TContext> middleware)
    {
        _middlewares.Add(middleware);
    }

    public async Task Execute(TContext input)
    {
        foreach (var middleware in _middlewares)
        {
            await middleware.Execute(input);
        }
    }
}
namespace MiddlewarePipelineLib;

public class Pipeline<TContext> where TContext : MiddlewareContext
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
            try
            {
                await middleware.Execute(context);
            }
            catch (Exception ex)
            {
                context.Errors.Add(ex);
            }
        }

        if (context.Errors.Count > 0)
        {
            this.HandleError(context.Errors);
        }

    }

    protected virtual void HandleError(IEnumerable<Exception> errors)
    {
        //handle errors
        foreach (var error in errors)
        {
            Console.Error.WriteLine(error);
        }
    }
}
namespace MiddlewarePipelineLib;

public class MiddlewarePipeline<TContext> where TContext : MiddlewareContext
{
    private readonly List<IMiddleware<TContext>> _middlewares = new();

    public void Use(IMiddleware<TContext> middleware)
    {
        _middlewares.Add(middleware);
    }

    public int Count => _middlewares.Count;

    public async Task Execute(TContext input)
    {
        Console.WriteLine("Begin pipeline...");

        foreach (var middleware in _middlewares)
        {
            try
            {
                await middleware.Execute(input);
            }
            catch (Exception ex)
            {
                input.Errors.Add(ex);
            }
        }

        if (input.Errors.Count > 0)
        {
            this.HandleError(input.Errors);
        }


        Console.WriteLine("End Pipeline");
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
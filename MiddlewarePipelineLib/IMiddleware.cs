namespace MiddlewarePipelineLib;

public interface IMiddleware<TContext> where TContext : MiddlewareContext
{
    Task Execute(TContext context);
}

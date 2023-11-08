namespace MiddlewarePipelineLib;

public interface IMiddleware<TContext> where TContext : Context
{
    Task Execute(TContext context);
}

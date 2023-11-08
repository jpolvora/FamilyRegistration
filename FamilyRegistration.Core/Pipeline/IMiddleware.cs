namespace FamilyRegistration.Core.Pipeline;

public interface IMiddleware<TContext> where TContext : MiddlewareContext
{
    Task Execute(TContext context);
}

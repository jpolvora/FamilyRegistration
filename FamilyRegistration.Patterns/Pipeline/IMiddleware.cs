namespace FamilyRegistration.Patterns.Pipeline;

public interface IMiddleware<TContext>
{
    Task Execute(TContext context);
}

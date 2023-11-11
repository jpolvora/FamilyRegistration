using FamilyRegistration.Patterns.Pipeline;

namespace FamilyRegistration.Core.Pipeline.Middlewares;

public class ThrowExceptionMiddleware : IMiddleware<FamilyContext>
{
    public Task Execute(FamilyContext context)
    {
        throw new NotImplementedException();
    }
}
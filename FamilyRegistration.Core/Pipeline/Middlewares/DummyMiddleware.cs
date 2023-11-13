using FamilyRegistration.Core.Domain;
using FamilyRegistration.Patterns.Pipeline;

namespace FamilyRegistration.Core.Pipeline.Middlewares;

public class DummyMiddleware : IMiddleware<FamilyContext>
{
    public Task Execute(FamilyContext context)
    {
        return Task.CompletedTask;
    }
}

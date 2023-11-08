using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Middlewares;

public class DummyMiddleware : IMiddleware<FamilyRegistrationContext>
{
    public Task Execute(FamilyRegistrationContext context)
    {
        return Task.CompletedTask;
    }
}

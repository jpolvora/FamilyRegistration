using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipeline.Middlewares;

public class DummyMiddleware : IMiddleware<FamilyRegistrationContext>
{
    public Task Execute(FamilyRegistrationContext context)
    {
        return Task.CompletedTask;
    }
}

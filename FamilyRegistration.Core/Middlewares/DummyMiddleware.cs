using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Middlewares;

public class DummyMiddleware : IMiddleware<FamilyContext>
{
    public Task Execute(FamilyContext context)
    {
        return Task.CompletedTask;
    }
}
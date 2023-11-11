using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipeline.Middlewares;

public class DummyMiddleware : IMiddleware<FamilyContext>
{
    public Task Execute(FamilyContext context)
    {
        return Task.CompletedTask;
    }
}

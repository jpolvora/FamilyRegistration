using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Middlewares;

public class ThrowExceptionMiddleware : IMiddleware<FamilyContext>
{
    public Task Execute(FamilyContext context)
    {
        throw new NotImplementedException();
    }
}
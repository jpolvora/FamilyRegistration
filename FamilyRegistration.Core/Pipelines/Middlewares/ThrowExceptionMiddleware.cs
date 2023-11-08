using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipelines.Middlewares;

public class ThrowExceptionMiddleware : IMiddleware<FamilyRegistrationContext>
{
    public Task Execute(FamilyRegistrationContext context)
    {
        throw new NotImplementedException();
    }
}
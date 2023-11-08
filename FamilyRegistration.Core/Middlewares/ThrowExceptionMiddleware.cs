using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Middlewares;

public class ThrowExceptionMiddleware : IMiddleware<FamilyRegistrationContext>
{
    public Task Execute(FamilyRegistrationContext context)
    {
        throw new NotImplementedException();
    }
}
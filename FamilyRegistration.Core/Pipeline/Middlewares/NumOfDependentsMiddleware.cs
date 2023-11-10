using FamilyRegistration.Core.Calculators;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipeline.Middlewares;

public class NumOfDependentsMiddleware : IMiddleware<FamilyRegistrationContext>
{
    public Task Execute(FamilyRegistrationContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

using FamilyRegistration.Core.Calculators;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipeline.Middlewares;

public class NumOfDependentsMiddleware : IMiddleware<FamilyContext>
{
    public Task Execute(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

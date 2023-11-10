using FamilyRegistration.Core.Calculators;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipelines.Middlewares;

public class NumOfDependentsMiddleware : IMiddleware<FamilyRegistrationContext>
{
    public Task Execute(FamilyRegistrationContext context)
    {
        var valueToIncrement = ScoreCalculators.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

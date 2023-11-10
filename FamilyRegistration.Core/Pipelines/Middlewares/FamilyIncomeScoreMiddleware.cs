using FamilyRegistration.Core.Calculators;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipelines.Middlewares;

public class FamilyIncomeScoreMiddleware : IMiddleware<FamilyRegistrationContext>
{
    public Task Execute(FamilyRegistrationContext context)
    {
        var valueToIncrement = ScoreCalculators.CalculateScoreByFamilyIncome(context.FamilyIncome);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipelines.Middlewares;

public class FamilyIncomeScoreMiddleware : IMiddleware<FamilyRegistrationContext>
{
    public Task Execute(FamilyRegistrationContext context)
    {
        var valueToIncrement = context.FamilyIncome switch
        {
            <= 900 => 5,
            > 900 and <= 1500 => 3,
            _ => 0
        };

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;

    }
}

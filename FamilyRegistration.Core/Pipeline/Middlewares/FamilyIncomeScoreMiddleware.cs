using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Core.Domain;
using FamilyRegistration.Patterns.Pipeline;

namespace FamilyRegistration.Core.Pipeline.Middlewares;

public class FamilyIncomeScoreMiddleware : IMiddleware<FamilyContext>
{
    public Task Execute(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByFamilyIncome(context.FamilyIncome);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

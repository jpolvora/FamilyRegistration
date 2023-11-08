using FamilyRegistration.Core.Pipeline;

namespace FamilyRegistration.Core.Middlewares;

public class FamilyIncomeScoreMiddleware : IMiddleware<FamilyContext>
{
    public Task Execute(FamilyContext context)
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

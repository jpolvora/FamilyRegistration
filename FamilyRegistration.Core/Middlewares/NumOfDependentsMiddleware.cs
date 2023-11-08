using FamilyRegistration.Core.Pipeline;

namespace FamilyRegistration.Core.Middlewares;

public class NumOfDependentsMiddleware : IMiddleware<FamilyContext>
{
    public Task Execute(FamilyContext context)
    {
        //switch (context.QtdeDependentes)
        //{
        //    case 0:
        //        break;
        //    case 1:
        //    case 2:
        //        context.IncrementScore(2);
        //        break;
        //    default:
        //        context.IncrementScore(3);
        //        break;
        //}

        var valueToIncrement = context.NumOfDependents switch
        {
            >= 1 and <= 2 => 2,
            >= 3 => 3,
            _ => 0

        };

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

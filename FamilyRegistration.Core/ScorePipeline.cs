using FamilyRegistration.Core.Middlewares;
using FamilyRegistration.Core.Pipeline;

namespace FamilyRegistration.Core;
public class ScorePipeline : MiddlewarePipeline<FamilyContext>
{
    public ScorePipeline()
    {
        this.Use(new NumOfDependentsMiddleware());
        this.Use(new FamilyIncomeScoreMiddleware());
    }
}

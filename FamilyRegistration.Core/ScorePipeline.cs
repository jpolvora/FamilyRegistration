using FamilyRegistration.Core.Middlewares;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core;
public class ScorePipeline : MiddlewarePipeline<FamilyContext>
{
    public ScorePipeline()
    {
        this.Use(new NumOfDependentsMiddleware());
        this.Use(new FamilyIncomeScoreMiddleware());
    }
}

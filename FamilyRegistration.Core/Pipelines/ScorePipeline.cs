using FamilyRegistration.Core.Middlewares;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipelines;
public class ScorePipeline : MiddlewarePipeline<FamilyRegistrationContext>
{
    public ScorePipeline()
    {
        Use(new NumOfDependentsMiddleware());
        Use(new FamilyIncomeScoreMiddleware());
        Use(new DummyMiddleware());
        Use(new ThrowExceptionMiddleware());
    }
}


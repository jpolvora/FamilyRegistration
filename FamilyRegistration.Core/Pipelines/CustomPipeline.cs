using FamilyRegistration.Core.Pipelines.Middlewares;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipelines;

public class CustomPipeline : MiddlewarePipeline<FamilyRegistrationContext>
{
    public CustomPipeline()
    {
        Use(new NumOfDependentsMiddleware());
        Use(new FamilyIncomeScoreMiddleware());
    }
}


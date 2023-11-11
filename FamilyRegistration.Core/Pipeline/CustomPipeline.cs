using FamilyRegistration.Core.Pipeline.Middlewares;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipeline;

public class CustomPipeline : MiddlewarePipeline<FamilyContext>
{
    public CustomPipeline()
    {
        Use(new NumOfDependentsMiddleware());
        Use(new FamilyIncomeScoreMiddleware());
    }
}


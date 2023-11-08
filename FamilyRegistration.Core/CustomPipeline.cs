using FamilyRegistration.Core.Middlewares;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core;

public class CustomPipeline : MiddlewarePipeline<FamilyContext>
{
    public CustomPipeline()
    {
        this.Use(new NumOfDependentsMiddleware());
        this.Use(new FamilyIncomeScoreMiddleware());
    }
}


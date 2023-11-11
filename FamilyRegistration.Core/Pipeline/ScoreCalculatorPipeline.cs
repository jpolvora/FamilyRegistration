using FamilyRegistration.Core.Pipeline.Middlewares;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipeline;

public class ScoreCalculatorPipeline : Pipeline<FamilyContext>
{
    public ScoreCalculatorPipeline()
    {
        Use(new NumOfDependentsMiddleware());
        Use(new FamilyIncomeScoreMiddleware());
    }
}


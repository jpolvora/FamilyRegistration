using FamilyRegistration.Core.Pipeline.Middlewares;
using FamilyRegistration.Patterns.Pipeline;

namespace FamilyRegistration.Core.Pipeline;

public class ScoreCalculatorPipeline : Pipeline<FamilyContext>
{
    public ScoreCalculatorPipeline()
    {
        Use(new NumOfDependentsMiddleware());
        Use(new FamilyIncomeScoreMiddleware());
    }

    public static ScoreCalculatorPipeline CreateTestPipeline()
    {
        var pipeline = new ScoreCalculatorPipeline();
        pipeline.Use(new DummyMiddleware());
        pipeline.Use(new ThrowExceptionMiddleware());

        return pipeline;
    }


}


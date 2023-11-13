using FamilyRegistration.Core.Domain;
using FamilyRegistration.Core.Pipeline.Middlewares;
using FamilyRegistration.Patterns.Pipeline;

namespace FamilyRegistration.Core.Pipeline;

public class ScoreCalculatorPipeline : Pipeline<FamilyContext>
{
    public ScoreCalculatorPipeline(IEnumerable<IMiddleware<FamilyContext>> middlewares)
    {
        foreach (var middleware in middlewares)
        {
            Use(middleware);
        }
    }

    public static ScoreCalculatorPipeline CreateProductionPipeline()
    {
        var middlewares = new IMiddleware<FamilyContext>[]
        {
            new FamilyIncomeScoreMiddleware(),
            new NumOfDependentsMiddleware()
        };

        return new ScoreCalculatorPipeline(middlewares);
    }

    public static ScoreCalculatorPipeline CreateTestPipeline()
    {
        var pipeline = CreateProductionPipeline();
        pipeline.Use(new DummyMiddleware());
        pipeline.Use(new ThrowExceptionMiddleware());

        return pipeline;
    }


}


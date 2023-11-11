using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Pipeline;
using FamilyRegistration.Core.Strategy;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Factory;
public static class ProcessData
{
    public static IProcessDataUseCase WithObservers()
    {
        return new ProcessDataUseCase(new ProcessDataWithObservers());
    }

    public static IProcessDataUseCase WithDecorators()
    {
        return new ProcessDataUseCase(new ProcessDataWithDecorator(new AggregateScoreCalculator()));
    }

    public static IProcessDataUseCase WithPipeline()
    {
        return new ProcessDataUseCase(new ProcessDataWithPipeline(ScoreCalculatorPipeline.CreateProductionPipeline()));
    }

    public static IProcessDataUseCase WithTransactionScript()
    {
        return new ProcessDataUseCase(new ProcessDataWithTransactionScript());
    }

    public static IProcessDataUseCase WithComposite()
    {
        return new ProcessDataUseCase(new ProcessDataWithComposite());
    }
}

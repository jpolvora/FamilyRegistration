using FamilyRegistration.Core.Decorators;

namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class ProcessarListaDecoratorUseCase : IProcessarListaUseCase
{
    private readonly IScoreCalculator _calculator;

    public ProcessarListaDecoratorUseCase(IScoreCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<ProcessarListaOutput> Execute(ProcessarListaInput input)
    {
        var contexts = input.Select(AdapterExtensions.Adapt);
        var report = new ProcessarListaOutput();
        foreach (var ctx in contexts)
        {
            await _calculator.Execute(ctx);
            report.Add(ctx.Adapt());
        }

        return report;
    }
}

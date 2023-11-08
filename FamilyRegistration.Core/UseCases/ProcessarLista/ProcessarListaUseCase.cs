using FamilyRegistration.Core.Middlewares;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class ProcessarListaUseCase : IProcessarListaUseCase
{
    private readonly MiddlewarePipeline<FamilyRegistrationContext> _pipeline;

    public ProcessarListaUseCase(MiddlewarePipeline<FamilyRegistrationContext> pipeline)
    {
        _pipeline = pipeline;
    }
    public async Task<ProcessarListaOutput> Execute(ProcessarListaInput input)
    {
        var contexts = input.Select(AdapterExtensions.Adapt);
        var report = new ProcessarListaOutput();
        foreach (var ctx in contexts)
        {
            await _pipeline.Execute(ctx);
            report.Add(ctx.Adapt());
        }

        return report;
    }
}

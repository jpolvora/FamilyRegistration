using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class ProcessarListaPipelineUseCase : IProcessarListaUseCase
{
    private readonly MiddlewarePipeline<FamilyRegistrationContext> _pipeline;

    public ProcessarListaPipelineUseCase(MiddlewarePipeline<FamilyRegistrationContext> pipeline)
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
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.UseCases;

public class ProcessarLista : IUseCase<ProcessarListaInput, ProcessarListaOutput>
{
    private readonly MiddlewarePipeline<FamilyContext> _pipeline;

    public ProcessarLista(MiddlewarePipeline<FamilyContext> pipeline)
    {
        _pipeline = pipeline;
    }
    public async Task<ProcessarListaOutput> Execute(ProcessarListaInput input)
    {
        var contexts = input.Data.Select(ModelToContextAdapter.Adapt);
        var report = new ProcessarListaOutput();
        foreach (var ctx in contexts)
        {
            await _pipeline.Execute(ctx);
            report.Data.Add(ContextToReportAdapter.Adapt(ctx));
        }

        return report;
    }
}

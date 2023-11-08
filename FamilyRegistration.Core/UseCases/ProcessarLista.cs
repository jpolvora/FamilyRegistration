namespace FamilyRegistration.Core.UseCases;

public class ProcessarLista : IUseCase<ProcessarListaInput, ProcessarListaOutput>
{
    public async Task<ProcessarListaOutput> Execute(ProcessarListaInput input)
    {
        var pipeline = new ScorePipeline();
        var contexts = input.Data.Select(ModelToContextAdapter.Adapt);
        var report = new ProcessarListaOutput();
        foreach (var ctx in contexts)
        {
            await pipeline.Execute(ctx);
            report.Data.Add(ContextToReportAdapter.Adapt(ctx));
        }

        return report;
    }
}

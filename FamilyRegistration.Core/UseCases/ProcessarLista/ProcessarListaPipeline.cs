using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class ProcessarListaPipeline : IProcessarListaUseCase
{
    private readonly MiddlewarePipeline<FamilyRegistrationContext> _pipeline;

    public ProcessarListaPipeline(MiddlewarePipeline<FamilyRegistrationContext> pipeline)
    {
        _pipeline = pipeline;
    }
    public async Task<Output> Execute(Input input)
    {
        var output = new Output();

        var contexts = input.Select(AdapterExtensions.Adapt);
        foreach (var ctx in contexts)
        {
            await _pipeline.Execute(ctx);
            output.Add(ctx.Adapt());
        }

        return output;
    }
}
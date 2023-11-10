using FamilyRegistration.Core.UseCases.ProcessData;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithPipeline : IProcessDataStrategy
{
    private readonly MiddlewarePipeline<FamilyRegistrationContext> _pipeline;

    public ProcessDataWithPipeline(MiddlewarePipeline<FamilyRegistrationContext> pipeline)
    {
        _pipeline = pipeline;
    }

    public async Task<Output> Execute(Input input)
    {
        var output = new Output();

        foreach (var inputItem in input)
        {
            var context = inputItem.AdaptToFamilyRegistrationContext();
            await _pipeline.Execute(context);

            var outputItem = context.AdaptToOutputItem();
            output.Add(outputItem);
        }

        return output;
    }
}

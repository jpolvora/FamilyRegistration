using FamilyRegistration.Core.Domain;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Pipeline;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithPipeline : IProcessDataStrategy
{
    private readonly Pipeline<FamilyContext> _pipeline;

    public ProcessDataWithPipeline(Pipeline<FamilyContext> pipeline)
    {
        _pipeline = pipeline;
    }

    public async Task<ProcessDataOutput> Execute(ProcessDataInput input)
    {
        var output = new ProcessDataOutput();

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

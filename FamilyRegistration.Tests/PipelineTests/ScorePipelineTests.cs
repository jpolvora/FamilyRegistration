using FamilyRegistration.Core.Pipeline;
using FluentAssertions;

namespace FamilyRegistration.Tests.PipelineTests;
public class ScorePipelineTests
{
    [Fact]
    public void PipelineSizeShouldBe4()
    {
        var pipeline = ScoreCalculatorPipeline.CreateTestPipeline();

        pipeline.Count.Should().Be(4);
    }
}

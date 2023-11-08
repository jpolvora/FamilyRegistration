﻿using FamilyRegistration.Core.Pipelines;
using FluentAssertions;

namespace FamilyRegistration.Tests.UnitTests;
public class ScorePipelineTests
{
    [Fact]
    public void PipelineSizeShouldBe4()
    {
        var pipeline = new ScorePipeline();

        pipeline.Count.Should().Be(4);
    }
}

﻿using FamilyRegistration.Core;
using FamilyRegistration.Core.Pipelines.Middlewares;
using FluentAssertions;

namespace FamilyRegistration.Tests.PipelineTests;

public class ThrowExceptionMiddlewareTests
{
    [Fact]
    public async void ShouldContainErrors()
    {
        var ctx = new FamilyRegistrationContext()
        {
            Key = Guid.NewGuid().ToString(),
        };

        var sut = new ThrowExceptionMiddleware();

        Func<Task> act = async () => await sut.Execute(ctx);
        await act.Should().ThrowAsync<NotImplementedException>();

        ctx.Errors.Should().HaveCount(0);
    }
}
using FamilyRegistration.Core;
using FamilyRegistration.Core.Middlewares;
using FluentAssertions;

namespace FamilyRegistration.Tests.UnitTests;

public class NumOfDependentesScoreMiddlewareTests
{
    [Fact]
    public async void ShouldReturn_0_When_NumOfDependents_Is_0()
    {
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 0,
        };

        var sut = new NumOfDependentsMiddleware();

        await sut.Execute(ctx);

        ctx.Score.Should().Be(0);
    }

    [Fact]
    public async void ShouldReturn_2_When_NumOfDependents_IsEqual_2()
    {
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 2,
        };

        var sut = new NumOfDependentsMiddleware();

        await sut.Execute(ctx);

        ctx.Score.Should().Be(2);
    }

    [Fact]
    public async void ShouldReturn_3_When_NumOfDependents_IsGreatherOrEqual_3()
    {
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 3,
        };

        var sut = new NumOfDependentsMiddleware();

        await sut.Execute(ctx);

        ctx.Score.Should().Be(3);
    }
}

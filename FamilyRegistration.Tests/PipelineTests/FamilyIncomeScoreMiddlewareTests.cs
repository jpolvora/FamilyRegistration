using FamilyRegistration.Core;
using FamilyRegistration.Core.Pipeline.Middlewares;
using FluentAssertions;

namespace FamilyRegistration.Tests.PipelineTests;

public class FamilyIncomeScoreMiddlewareTests
{
    [Fact]
    public async void ShouldReturn_3_When_Income_Is_901()
    {
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            FamilyIncome = 901
        };

        var sut = new FamilyIncomeScoreMiddleware();

        await sut.Execute(ctx);

        ctx.Score.Should().Be(3);
    }

    [Fact]
    public async void ShouldReturn_5_When_Income_Is_900()
    {
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            FamilyIncome = 900
        };

        var sut = new FamilyIncomeScoreMiddleware();

        await sut.Execute(ctx);

        ctx.Score.Should().Be(5);
    }

    [Fact]
    public async void ShouldReturn_5_When_Income_Is_Zero()
    {
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            FamilyIncome = 0
        };

        var sut = new FamilyIncomeScoreMiddleware();

        await sut.Execute(ctx);

        ctx.Score.Should().Be(5);
    }

    [Fact]
    public async void ShouldReturn_Zero_When_Income_IsGreatherThan_1500()
    {
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            FamilyIncome = 1501
        };

        var sut = new FamilyIncomeScoreMiddleware();

        await sut.Execute(ctx);

        ctx.Score.Should().Be(0);
    }
}

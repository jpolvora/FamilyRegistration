using FamilyRegistration.Core.Domain;
using FamilyRegistration.Core.Pipeline;
using FamilyRegistration.Core.Pipeline.Middlewares;
using FamilyRegistration.Patterns.Pipeline;
using FluentAssertions;

namespace FamilyRegistration.Tests.PipelineTests;

public class ScoreTests
{
    [Fact]
    public async void ScoreShoulbBeEqual5()
    {
        //arrange
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 1,
            FamilyIncome = 901
        };

        var pipeline = new Pipeline<FamilyContext>();
        pipeline.Use(new FamilyIncomeScoreMiddleware());
        pipeline.Use(new NumOfDependentsMiddleware());

        //act

        await pipeline.Execute(ctx);

        //assert
        ctx.Score.Should().Be(5);
    }

    [Fact]
    public async void ScoreShoudBeEqual3()
    {
        //arrange
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 5,
            FamilyIncome = 2000
        };

        var pipeline = new Pipeline<FamilyContext>();
        pipeline.Use(new FamilyIncomeScoreMiddleware());
        pipeline.Use(new NumOfDependentsMiddleware());

        //act

        await pipeline.Execute(ctx);

        //assert
        ctx.Score.Should().Be(3);

    }

    [Fact]
    public async void ScoreShoudBeEqual7()
    {
        //arrange
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 2,
            FamilyIncome = 900
        };

        var pipeline = new Pipeline<FamilyContext>();
        pipeline.Use(new FamilyIncomeScoreMiddleware());
        pipeline.Use(new NumOfDependentsMiddleware());

        //act

        await pipeline.Execute(ctx);

        //assert
        ctx.Score.Should().Be(7);

    }


    [Fact]
    public async void ScoreShoudBeEqual8()
    {
        //arrange
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 3,
            FamilyIncome = 899
        };

        var pipeline = new Pipeline<FamilyContext>();
        pipeline.Use(new FamilyIncomeScoreMiddleware());
        pipeline.Use(new NumOfDependentsMiddleware());

        //act

        await pipeline.Execute(ctx);

        //assert
        ctx.Score.Should().Be(8);
    }

    [Fact]
    public async void ScoreShoudBeEqualZero()
    {
        //arrange
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 0,
            FamilyIncome = 1501
        };

        var pipeline = new Pipeline<FamilyContext>();
        pipeline.Use(new FamilyIncomeScoreMiddleware());
        pipeline.Use(new NumOfDependentsMiddleware());

        //act

        await pipeline.Execute(ctx);

        //assert
        ctx.Score.Should().Be(0);
    }

    [Fact]
    public async void ScoreShoudBeEqual6()
    {
        //arrange
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 4,
            FamilyIncome = 1111
        };

        var pipeline = ScoreCalculatorPipeline.CreateTestPipeline();

        //act

        Func<Task> act = async () => await pipeline.Execute(ctx);

        await act.Should().ThrowAsync<NotImplementedException>();
        //assert
        ctx.Score.Should().Be(6);
    }

    [Fact]
    public async void ShouldHaveTwoErrors()
    {
        //arrange
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 4,
            FamilyIncome = 1111
        };

        var pipeline = ScoreCalculatorPipeline.CreateTestPipeline();
        pipeline.Use(new ThrowExceptionMiddleware());

        //act
        Func<Task> act = async () => await pipeline.Execute(ctx);
        await act.Should().ThrowAsync<NotImplementedException>();


        //assert
        ctx.Score.Should().Be(6);
    }
}

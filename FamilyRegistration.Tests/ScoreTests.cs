using FamilyRegistration.Core;
using FamilyRegistration.Core.Middlewares;
using FluentAssertions;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Tests;

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

        var pipeline = new MiddlewarePipeline<FamilyContext>();
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

        var pipeline = new MiddlewarePipeline<FamilyContext>();
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

        var pipeline = new MiddlewarePipeline<FamilyContext>();
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
        var row = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 3,
            FamilyIncome = 899
        };

        var pipeline = new MiddlewarePipeline<FamilyContext>();
        pipeline.Use(new FamilyIncomeScoreMiddleware());
        pipeline.Use(new NumOfDependentsMiddleware());

        //act

        await pipeline.Execute(row);

        //assert
        row.Score.Should().Be(8);
    }

    [Fact]
    public async void ScoreShoudBeEqualZero()
    {
        //arrange
        var row = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 0,
            FamilyIncome = 1501
        };

        var pipeline = new MiddlewarePipeline<FamilyContext>();
        pipeline.Use(new FamilyIncomeScoreMiddleware());
        pipeline.Use(new NumOfDependentsMiddleware());

        //act

        await pipeline.Execute(row);

        //assert
        row.Score.Should().Be(0);
    }

    [Fact]
    public async void ScoreShoudBeEqual6()
    {
        //arrange
        var row = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 4,
            FamilyIncome = 1111
        };

        var pipeline = new ScorePipeline();

        //act

        await pipeline.Execute(row);

        //assert
        row.Score.Should().Be(6);
    }
}

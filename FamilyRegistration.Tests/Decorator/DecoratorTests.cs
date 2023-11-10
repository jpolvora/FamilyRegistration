﻿using FamilyRegistration.Core;
using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Decorator.Calculators;
using FluentAssertions;

namespace FamilyRegistration.Tests.Decorator;
public class DecoratorTests
{
    public DecoratorTests()
    {

    }

    private static ScoreCalculator GetScoreCalculatorDefault()
    {
        //IScoreCalculator calculator = new NumOfDependetsDecorator(new FamilyIncomeDecorator(new ScoreCalculator()));
        return new AggregateScoreCalculator();
    }

    [Fact]
    public async void CalcScoreUsingDecoratorsShouldReturn_7()
    {
        //arrange 
        var ctx = new FamilyRegistrationContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 2,
            FamilyIncome = 900
        };

        var scoreCalculator = GetScoreCalculatorDefault();

        //act
        await scoreCalculator.Execute(ctx);

        //assert
        ctx.Score.Should().Be(7);
        ctx.Errors.Should().HaveCount(0);
    }

    [Fact]
    public async void CalcScoreUsingDecoratorsShouldReturn_8()
    {
        //arrange 
        //arrange
        var ctx = new FamilyRegistrationContext()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 3,
            FamilyIncome = 899
        };

        var scoreCalculator = GetScoreCalculatorDefault();

        //act
        await scoreCalculator.Execute(ctx);

        //assert
        ctx.Score.Should().Be(8);
        ctx.Errors.Should().HaveCount(0);
    }

    //arrange


}

using FamilyRegistration.Core.Factory;
using FamilyRegistration.Core.UseCases.ProcessData;
using FluentAssertions;

namespace FamilyRegistration.Tests.Composite;

public class CompositeTests
{
    [Fact]
    public async void CalcScoreUsingCompositeShouldBe7()
    {
        //arrange 
        var inputItem = new ProcessDataInputItem()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 2,
            FamilyIncome = 900
        };

        var useCase = ProcessData.WithComposite();
        //act
        var input = new ProcessDataInput(new[] { inputItem });
        var output = await useCase.Execute(input);

        output.Count.Should().Be(1);

        var outputItem = output.Single();

        //assert
        outputItem.Score.Should().Be(7);
    }

    [Fact]
    public async void CalcScoreUsingCompositeShouldBe8()
    {
        //arrange 
        var inputItem = new ProcessDataInputItem()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 3,
            FamilyIncome = 899
        };

        var useCase = ProcessData.WithComposite();

        //act
        var input = new ProcessDataInput(new[] { inputItem });
        var output = await useCase.Execute(input);

        output.Count.Should().Be(1);

        var outputItem = output.Single();

        //assert
        outputItem.Score.Should().Be(8);
    }

    [Fact]
    public async void CalcScoreUsingCompositeShouldBe3()
    {
        //arrange 
        var inputItem = new ProcessDataInputItem()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 5,
            FamilyIncome = 2000
        };

        var useCase = ProcessData.WithComposite();

        //act
        var input = new ProcessDataInput(new[] { inputItem });
        var output = await useCase.Execute(input);

        output.Count.Should().Be(1);

        var outputItem = output.Single();

        //assert
        outputItem.Score.Should().Be(3);
    }


}

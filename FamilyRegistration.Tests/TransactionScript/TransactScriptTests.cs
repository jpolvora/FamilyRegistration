using FamilyRegistration.Core.Factory;
using FamilyRegistration.Core.UseCases.ProcessData;
using FluentAssertions;

namespace FamilyRegistration.Tests.TransactionScript;
public class TransactScriptTests
{
    [Fact]
    public async void CalcScoreUsingTransactionScriptShouldBe7()
    {
        //arrange 
        var inputItem = new InputItem()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 2,
            FamilyIncome = 900
        };

        var useCase = ProcessData.WithTransactionScript();

        //act
        var input = new Input(new[] { inputItem });
        var output = await useCase.Execute(input);

        output.Count.Should().Be(1);

        var outputItem = output.Single();

        //assert
        outputItem.Score.Should().Be(7);
    }

    [Fact]
    public async void CalcScoreUsingTransactionScriptShouldBe5()
    {
        //arrange 
        var inputItem = new InputItem()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 1,
            FamilyIncome = 901
        };

        var useCase = ProcessData.WithTransactionScript();

        //act
        var input = new Input(new[] { inputItem });
        var output = await useCase.Execute(input);

        output.Count.Should().Be(1);

        var outputItem = output.Single();

        //assert
        outputItem.Score.Should().Be(5);
    }

    [Fact]
    public async void CalcScoreUsingTransactionScriptShouldBe3()
    {
        //arrange 
        var inputItem = new InputItem()
        {
            Key = Guid.NewGuid().ToString(),
            NumOfDependents = 5,
            FamilyIncome = 2000
        };

        var useCase = ProcessData.WithTransactionScript();

        //act
        var input = new Input(new[] { inputItem });
        var output = await useCase.Execute(input);

        output.Count.Should().Be(1);

        var outputItem = output.Single();

        //assert
        outputItem.Score.Should().Be(3);
    }


}

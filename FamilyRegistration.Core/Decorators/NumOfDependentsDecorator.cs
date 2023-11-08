namespace FamilyRegistration.Core.Decorators;

public class NumOfDependentsDecorator : BaseDecorator
{
    public NumOfDependentsDecorator(IScoreCalculator wrapped) : base(wrapped)
    {
    }

    public override Task Execute(FamilyRegistrationContext context)
    {
        var valueToIncrement = context.NumOfDependents switch
        {
            >= 1 and <= 2 => 2,
            >= 3 => 3,
            _ => 0

        };

        context.IncrementScore(valueToIncrement);

        return base.Execute(context);
    }
}

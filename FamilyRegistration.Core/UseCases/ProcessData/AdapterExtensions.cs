namespace FamilyRegistration.Core.UseCases.ProcessData;

public static class AdapterExtensions
{
    public static FamilyRegistrationContext AdaptToFamilyRegistrationContext(this InputItem model)
    {
        return new FamilyRegistrationContext()
        {
            Key = model.Key,
            FamilyIncome = model.FamilyIncome,
            NumOfDependents = model.NumOfDependents
        };
    }

    public static OutputItem AdaptToOutputItem(this FamilyRegistrationContext context)
    {
        return new OutputItem()
        {
            Key = context.Key,
            FamilyIncome = context.FamilyIncome,
            NumOfDependents = context.NumOfDependents,
            Score = context.Score
        };
    }

    public static Input AsInput(this IEnumerable<InputItem> inputItems)
    {
        return new Input(inputItems);
    }
}

namespace FamilyRegistration.Core.UseCases.ProcessData;

public static class AdapterExtensions
{
    public static FamilyRegistrationContext AdaptToFamilyRegistrationContext(this ProcessDataInputItem model)
    {
        return new FamilyRegistrationContext()
        {
            Key = model.Key,
            FamilyIncome = model.FamilyIncome,
            NumOfDependents = model.NumOfDependents
        };
    }

    public static ProcessDataOutputItem AdaptToOutputItem(this FamilyRegistrationContext context)
    {
        return new ProcessDataOutputItem()
        {
            Key = context.Key,
            FamilyIncome = context.FamilyIncome,
            NumOfDependents = context.NumOfDependents,
            Score = context.Score
        };
    }

    public static ProcessDataInput AsInput(this IEnumerable<ProcessDataInputItem> inputItems)
    {
        return new ProcessDataInput(inputItems);
    }
}

namespace FamilyRegistration.Core.UseCases;

public class ModelToContextAdapter
{
    public static FamilyContext Adapt(Model model)
    {
        return new FamilyContext()
        {
            Key = model.Key,
            FamilyIncome = model.FamilyIncome,
            NumOfDependents = model.NumOfDependents
        };
    }
}

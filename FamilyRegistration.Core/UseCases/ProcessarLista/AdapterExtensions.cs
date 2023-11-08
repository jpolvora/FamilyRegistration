namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public static class AdapterExtensions
{
    public static FamilyRegistrationContext Adapt(this FamilyDTO model)
    {
        return new FamilyRegistrationContext()
        {
            Key = model.Key,
            FamilyIncome = model.FamilyIncome,
            NumOfDependents = model.NumOfDependents
        };
    }

    public static ReportRow Adapt(this FamilyRegistrationContext context)
    {
        return new ReportRow()
        {
            Key = context.Key,
            FamilyIncome = context.FamilyIncome,
            NumOfDependents = context.NumOfDependents,
            Score = context.Score
        };
    }
}

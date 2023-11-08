namespace FamilyRegistration.Core.UseCases;

public class ContextToReportAdapter
{
    public static Report Adapt(FamilyContext context)
    {
        return new Report()
        {
            Key = context.Key,
            FamilyIncome = context.FamilyIncome,
            NumOfDependents = context.NumOfDependents,
            Score = context.Score
        };
    }
}

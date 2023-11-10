namespace FamilyRegistration.Core.Calculators;

public static class ScoreCalculators
{
    public static int CalculateScoreByFamilyIncome(decimal familyIncome)
    {
        var valueToIncrement = familyIncome switch
        {
            <= 900 => 5,
            > 900 and <= 1500 => 3,
            _ => 0
        };

        return valueToIncrement;
    }

    public static int CalculateScoreByNumOfDependents(int numOfDependents)
    {
        //switch (context.QtdeDependentes)
        //{
        //    case 0:
        //        break;
        //    case 1:
        //    case 2:
        //        context.IncrementScore(2);
        //        break;
        //    default:
        //        context.IncrementScore(3);
        //        break;
        //}

        var valueToIncrement = numOfDependents switch
        {
            >= 1 and <= 2 => 2,
            >= 3 => 3,
            _ => 0
        };

        return valueToIncrement;
    }
}

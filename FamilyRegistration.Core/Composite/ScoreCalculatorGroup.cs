namespace FamilyRegistration.Core.Composite;

public class ScoreCalculatorGroup : ScoreCalculatorComponent
{
    protected readonly List<ScoreCalculatorComponent> _calculators = new();

    public override async Task<int> CalculateScore(FamilyContext context)
    {
        var score = 0;
        foreach (var calc in _calculators)
        {
            score += await calc.CalculateScore(context);
        }

        return score;
    }

    public void Add(ScoreCalculatorComponent composableScoreCalculator) => _calculators.Add(composableScoreCalculator);

    public void Remove(ScoreCalculatorComponent composableScoreCalculator) => _calculators.Remove(composableScoreCalculator);
}
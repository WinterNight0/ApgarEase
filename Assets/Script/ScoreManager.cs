using System.Collections.Generic;

public static class ScoreManager
{
    // Will hold exactly three totals: 1-min, 5-min, and 10-min
    public static List<int> RoundTotals { get; } = new List<int>();

    public static List<Dictionary<string, int>> RoundCategoryScores { get; } = new List<Dictionary<string, int>>();
}

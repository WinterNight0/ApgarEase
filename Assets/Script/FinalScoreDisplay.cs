using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FinalScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fullReportText;

    void Start()
    {
        var totals = ScoreManager.RoundTotals;
        var breakdowns = ScoreManager.RoundCategoryScores;

        if (totals.Count < 3 || breakdowns.Count < 3)
        {
            fullReportText.text = "Not enough data to display full Apgar summary.";
            return;
        }

        string FormatRound(int index, string label)
        {
            var round = breakdowns[index];
            string result = $"��ṹ {label} �ҷ� : {totals[index]}\n";
            result += $"- �ռ�� (Appearance) : {round["Appearance"]}\n";
            result += $"- �վ�� (Pulse) : {round["Pulse"]}\n";
            result += $"- ��õͺʹͧ (Grimace) : {round["Grimace"]}\n";
            result += $"- �������͹��� (Activity) : {round["Activity"]}\n";
            result += $"- ������� (Respiration) : {round["Respiration"]}\n";
            return result;
        }

        fullReportText.text =
            FormatRound(0, "1") + "\n" +
            FormatRound(1, "5") + "\n" +
            FormatRound(2, "10");
    }
}

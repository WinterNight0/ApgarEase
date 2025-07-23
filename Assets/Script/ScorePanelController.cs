using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScorePanelController : MonoBehaviour
{
    public bool HasSubmitted { get; private set; }

    // Appearance buttons
    [SerializeField] private Button app0Button;
    [SerializeField] private Button app1Button;
    [SerializeField] private Button app2Button;
    // Pulse buttons
    [SerializeField] private Button pulse0Button;
    [SerializeField] private Button pulse1Button;
    [SerializeField] private Button pulse2Button;
    // Grimace buttons
    [SerializeField] private Button grimace0Button;
    [SerializeField] private Button grimace1Button;
    [SerializeField] private Button grimace2Button;
    // Activity buttons
    [SerializeField] private Button activity0Button;
    [SerializeField] private Button activity1Button;
    [SerializeField] private Button activity2Button;
    // Respiration buttons
    [SerializeField] private Button resp0Button;
    [SerializeField] private Button resp1Button;
    [SerializeField] private Button resp2Button;

    [SerializeField] private Button submitButton;

    private enum Category { Appearance, Pulse, Grimace, Activity, Respiration }

    // Holds current five category scores before submission
    private Dictionary<Category, int> currentScores = new Dictionary<Category, int>()
    {
        { Category.Appearance,   -1 },
        { Category.Pulse,        -1 },
        { Category.Grimace,      -1 },
        { Category.Activity,     -1 },
        { Category.Respiration,  -1 },
    };

    // Stores snapshots of each round's scores
    private List<Dictionary<Category, int>> allScores = new List<Dictionary<Category, int>>();
    private bool isInteractionActive = false;

    void Awake()
    {
        app0Button.onClick.AddListener(() => OnScoreSelected(Category.Appearance, 0));
        app1Button.onClick.AddListener(() => OnScoreSelected(Category.Appearance, 1));
        app2Button.onClick.AddListener(() => OnScoreSelected(Category.Appearance, 2));
        pulse0Button.onClick.AddListener(() => OnScoreSelected(Category.Pulse, 0));
        pulse1Button.onClick.AddListener(() => OnScoreSelected(Category.Pulse, 1));
        pulse2Button.onClick.AddListener(() => OnScoreSelected(Category.Pulse, 2));
        grimace0Button.onClick.AddListener(() => OnScoreSelected(Category.Grimace, 0));
        grimace1Button.onClick.AddListener(() => OnScoreSelected(Category.Grimace, 1));
        grimace2Button.onClick.AddListener(() => OnScoreSelected(Category.Grimace, 2));
        activity0Button.onClick.AddListener(() => OnScoreSelected(Category.Activity, 0));
        activity1Button.onClick.AddListener(() => OnScoreSelected(Category.Activity, 1));
        activity2Button.onClick.AddListener(() => OnScoreSelected(Category.Activity, 2));
        resp0Button.onClick.AddListener(() => OnScoreSelected(Category.Respiration, 0));
        resp1Button.onClick.AddListener(() => OnScoreSelected(Category.Respiration, 1));
        resp2Button.onClick.AddListener(() => OnScoreSelected(Category.Respiration, 2));

        submitButton.onClick.AddListener(OnSubmit);

        DisableInteraction();
    }

    private void OnScoreSelected(Category cat, int value)
    {
        currentScores[cat] = value;
        Debug.Log($"[APGAR] {cat} = {value}");
    }

    private void OnSubmit()
    {
        foreach (var kv in currentScores)
        {
            if (kv.Value < 0)
            {
                Debug.LogWarning("Please select a score for all APGAR categories before submitting.");
                return;
            }
        }

        var roundCopy = new Dictionary<Category, int>(currentScores);
        allScores.Add(roundCopy);

        int roundTotal = 0;
        foreach (var score in roundCopy.Values)
            roundTotal += score;

        ScoreManager.RoundTotals.Add(roundTotal);

        // Store full category scores with readable keys
        Dictionary<string, int> readable = new Dictionary<string, int>();
        foreach (var kv in roundCopy)
            readable.Add(kv.Key.ToString(), kv.Value);
        ScoreManager.RoundCategoryScores.Add(readable);

        Debug.Log($"[APGAR] Round {allScores.Count} submitted. Total = {roundTotal}");

        HasSubmitted = true;
        DisableInteraction();

        if (allScores.Count >= 3)
        {
            SceneManager.LoadScene("Final");
        }
    }

    public int[] GetAllTotals()
    {
        int[] totals = new int[allScores.Count];
        for (int i = 0; i < allScores.Count; i++)
        {
            int sum = 0;
            foreach (var s in allScores[i].Values)
                sum += s;
            totals[i] = sum;
        }
        return totals;
    }

    public Dictionary<string, int> GetCategoryScoresForRound(int roundIndex)
    {
        if (roundIndex < 0 || roundIndex >= allScores.Count)
            return null;

        Dictionary<Category, int> round = allScores[roundIndex];
        Dictionary<string, int> readableScores = new Dictionary<string, int>();

        foreach (var kv in round)
        {
            readableScores.Add(kv.Key.ToString(), kv.Value);
        }

        return readableScores;
    }

    public void EnableInteraction()
    {
        if (isInteractionActive) return;
        isInteractionActive = true;

        HasSubmitted = false;
        var keys = new List<Category>(currentScores.Keys);
        foreach (var key in keys)
            currentScores[key] = -1;

        SetButtonsInteractable(true);
    }

    public void DisableInteraction()
    {
        isInteractionActive = false;
        SetButtonsInteractable(false);
    }

    private void SetButtonsInteractable(bool interactable)
    {
        var allButtons = new[]
        {
            app0Button, app1Button, app2Button,
            pulse0Button, pulse1Button, pulse2Button,
            grimace0Button, grimace1Button, grimace2Button,
            activity0Button, activity1Button, activity2Button,
            resp0Button, resp1Button, resp2Button,
            submitButton
        };

        foreach (var btn in allButtons)
            btn.interactable = interactable;
    }
}

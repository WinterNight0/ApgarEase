using UnityEngine;

public class PrefabActivator : MonoBehaviour
{
    [SerializeField] private Timer timerRef;
    [SerializeField] private GameObject prefabToActivate;

    private GameObject instantiatedPrefab;
    private ScorePanelController scorePanel;

    private enum Phase { Waiting, First, Second, Third, Done }
    private Phase currentPhase = Phase.Waiting;

    void Start()
    {
        instantiatedPrefab = Instantiate(prefabToActivate);
        instantiatedPrefab.SetActive(false);
        scorePanel = instantiatedPrefab.GetComponent<ScorePanelController>();
    }

    void Update()
    {
        float time = timerRef.GetElapsedTime();

        switch (currentPhase)
        {
            case Phase.Waiting:
                if (time >= 60f) //60f
                {
                    instantiatedPrefab.SetActive(true);
                    scorePanel.EnableInteraction();
                    currentPhase = Phase.First;
                }
                break;

            case Phase.First:
                if (scorePanel.HasSubmitted)
                {
                    scorePanel.DisableInteraction();
                    currentPhase = Phase.Second;
                }
                break;

            case Phase.Second:
                if (time >= 300f) //300f
                {
                    scorePanel.EnableInteraction();
                    currentPhase = Phase.Third;
                }
                break;

            case Phase.Third:
                if (scorePanel.HasSubmitted)
                {
                    scorePanel.DisableInteraction();
                    currentPhase = Phase.Done;
                }
                break;

            case Phase.Done:
                if (time >= 600f) //600f
                {
                    scorePanel.EnableInteraction();
                }
                break;
        }
    }
}

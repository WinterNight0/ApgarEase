using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OneButtonTwoChoice : MonoBehaviour
{
    [Header("Timer & Control")]
    [SerializeField] private Timer timer;
    [SerializeField] private Button triggerButton;
    [SerializeField] private float delayBeforeChoices = 15f;

    [Header("Option Buttons (Will Enable After Delay)")]
    [SerializeField] private Button optionButtonA;
    [SerializeField] private string sceneNameA = "Scene_A";

    [SerializeField] private Button optionButtonB;
    [SerializeField] private string sceneNameB = "Scene_B";

    private bool isTiming = false;
    private float startTime;
    private bool choicesEnabled = false;

    void Start()
    {
        if (triggerButton != null)
            triggerButton.onClick.AddListener(OnPPVStartClicked);

        // Disable both option buttons initially
        if (optionButtonA != null) optionButtonA.interactable = false;
        if (optionButtonB != null) optionButtonB.interactable = false;

        // Assign listeners (won't trigger until interactable is true)
        if (optionButtonA != null)
            optionButtonA.onClick.AddListener(() => SceneManager.LoadScene(sceneNameA));

        if (optionButtonB != null)
            optionButtonB.onClick.AddListener(() => SceneManager.LoadScene(sceneNameB));
    }

    void Update()
    {
        if (isTiming && timer.GetElapsedTime() - startTime >= delayBeforeChoices)
        {
            timer.Pause();
            isTiming = false;

            if (!choicesEnabled)
            {
                if (optionButtonA != null) optionButtonA.interactable = true;
                if (optionButtonB != null) optionButtonB.interactable = true;
                choicesEnabled = true;
            }
        }
    }

    private void OnPPVStartClicked()
    {
        timer.ResetTimer();
        timer.Resume();
        startTime = timer.GetElapsedTime();
        isTiming = true;

        if (triggerButton != null)
            triggerButton.interactable = false;
    }
}

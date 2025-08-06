using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class ButtonSceneLink
{
    public Button button;                  // Button to control
    public string sceneName;               // Scene name to load
    public bool useTimerUnlock = false;    // Should this button wait for timer?
    public float unlockTime = 0f;          // Time in seconds to unlock
}

public class ButtonSceneLinker : MonoBehaviour
{
    [Header("List of Buttons with Their Target Scenes")]
    [SerializeField] private List<ButtonSceneLink> buttonSceneLinks = new List<ButtonSceneLink>();

    [Header("Optional Timer Reference")]
    [SerializeField] private Timer timer; // Drag your Timer here if using timed unlocks

    private void Start()
    {
        foreach (var link in buttonSceneLinks)
        {
            if (link.button == null) continue;

            // If using timer to unlock = disable button at start
            if (link.useTimerUnlock)
                link.button.interactable = false;

            // Capture local variable for scene loading
            string sceneToLoad = link.sceneName;
            link.button.onClick.AddListener(() => SceneManager.LoadScene(sceneToLoad));
        }
    }

    private void Update()
    {
        if (timer == null) return; // No timer = nothing to check

        float currentTime = timer.GetElapsedTime();

        foreach (var link in buttonSceneLinks)
        {
            if (link.useTimerUnlock && !link.button.interactable && currentTime >= link.unlockTime)
            {
                link.button.interactable = true; // Unlock button
            }
        }
    }
}

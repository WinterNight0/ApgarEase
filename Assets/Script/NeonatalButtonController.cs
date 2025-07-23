using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NeonatalButtonController : MonoBehaviour
{
    [Tooltip("Drag your Neonatal Resuscitation UI Button here")]
    [SerializeField] private Button neonatalButton;

    [Tooltip("Reference to your Timer script")]
    [SerializeField] private Timer timerRef;

    [Tooltip("Delay before enabling the Neonatal button (in seconds)")]
    [SerializeField] private float unlockDelay = 15f;

    private bool hasUnlocked = false;

    void Awake()
    {
        if (neonatalButton == null || timerRef == null)
        {
            Debug.LogError("NeonatalButtonController: please assign both neonatalButton and timerRef in the Inspector.");
            enabled = false;
            return;
        }

        neonatalButton.interactable = false;
        neonatalButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        if (hasUnlocked) return;

        if (timerRef.GetElapsedTime() >= unlockDelay)
        {
            UnlockButton();
        }
    }

    private void UnlockButton()
    {
        hasUnlocked = true;
        neonatalButton.interactable = true;
        neonatalButton.onClick.AddListener(() => SceneManager.LoadScene("Neonatal"));
        Debug.Log($"Neonatal button unlocked at {timerRef.GetElapsedTime():0.00}s");
    }
}

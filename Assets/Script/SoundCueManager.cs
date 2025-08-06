using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SoundCue
{
    public AudioSource audioSource;       // Drag AudioSource from scene
    public float triggerTimeInSeconds;    // Set time when to play
    [HideInInspector] public bool hasPlayed = false; // Internal
}

public class SoundCueManager : MonoBehaviour
{
    [SerializeField] private Timer timer; // Reference to Timer script
    [SerializeField] private List<SoundCue> soundCues = new List<SoundCue>();

    private float lastTime = 0f;

    void Update()
    {
        if (timer == null) return;

        float currentTime = timer.GetElapsedTime();

        // Optional: Reset if timer is reset manually
        if (currentTime < lastTime)
        {
            ResetCues();
        }

        lastTime = currentTime;

        foreach (var cue in soundCues)
        {
            if (!cue.hasPlayed && cue.audioSource != null && currentTime >= cue.triggerTimeInSeconds)
            {
                cue.audioSource.Play();
                cue.hasPlayed = true;
            }
        }
    }

    /// <summary>
    /// Call this manually if you reset the timer and want cues to play again.
    /// </summary>
    public void ResetCues()
    {
        foreach (var cue in soundCues)
        {
            cue.hasPlayed = false;
        }
    }
}

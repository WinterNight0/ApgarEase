using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundAtTime : MonoBehaviour
{
    [SerializeField] private Timer timer; // Reference to Timer script
    [SerializeField] private AudioSource soundAt15Sec; // Sound at 15 sec
    [SerializeField] private AudioSource soundAt1Min; // Sound at 1 min
    [SerializeField] private AudioSource soundAt5Min; // Sound at 5 min
    [SerializeField] private AudioSource soundAt10Min; // Sound at 10 min

    private bool played15Sec = false;
    private bool played1Min = false;
    private bool played5Min = false;
    private bool played10Min = false;

    void Update()
    {
        float time = timer.GetElapsedTime();

        // Play sound at 15 seconds
        if (!played15Sec && time >= 15f)
        {
            soundAt15Sec.Play();
            played15Sec = true;
        }

        // Play sound and change scene at 1 minute
        if (!played1Min && time >= 60f) //60f number in script is for test
        {   
            soundAt1Min.Play();
            played1Min = true;
        }

        // Play sound and change scene at 5 minutes
        if (!played5Min && time >= 300f) //300f number in script is for test
        {
            soundAt5Min.Play();
            played5Min = true;
        }
        // Play sound and change scene at 10 minutes
        if (!played10Min && time >= 600f) //600f number in script is for test
        {
            soundAt10Min.Play();
            played10Min = true;
        }
    }
}

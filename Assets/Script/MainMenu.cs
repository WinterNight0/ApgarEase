using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartProgram()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Process");
    }
}

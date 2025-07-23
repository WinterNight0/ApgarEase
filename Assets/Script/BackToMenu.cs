using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public void StartProgram()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu_Scene");
    }
}

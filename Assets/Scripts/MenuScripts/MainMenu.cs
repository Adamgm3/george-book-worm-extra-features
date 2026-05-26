using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// TitleScreen = Scene 0
    /// </summary>
    public void TitleScreen()
    {
        //TitleScreen = Scene 0
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// HubWorld = Scene 1
    /// </summary>
    public void StartGame()
    {
        //HubWorld = Scene 0
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Exits Game Normally
    /// </summary>
    public void ExitGame()
    {
        //ExitGame0
        Debug.Log("Quitting Game");

        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    /// <summary>
    /// Takes the world the user has interacted to go to, and then sends them there
    /// World 1 = Scene 2
    /// </summary>
    /// <param name="worldNum"></param>
    public void LoadWorld(int worldNum)
    {
        switch (worldNum)
        {
            case 1:
                SceneManager.LoadScene(2);
                break;
            case 2:
                SceneManager.LoadScene(3);
                break;
            default:
                SceneManager.LoadScene(0);
                break;
        }
    }
}

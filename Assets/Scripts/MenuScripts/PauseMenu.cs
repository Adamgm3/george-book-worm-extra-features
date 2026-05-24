using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    bool isPaused = false;

    private void Start()
    {
        panel.SetActive(false);
    }
    private void Update()
    {
        //When player hits ESC or other decided key
        if (Input.GetKey(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Pause()
    {
        //Pauses game on esc pressed
        panel.SetActive(true);

        Time.timeScale = 0f;

        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Resume()
    {
        //Resumes the game from being paused
        panel.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToHub()
    {
        //Placeholder for potential return to hub method
    }

    public void Save()
    {
        //Placeholder for Save Code
    }
    public void Load()
    {
        //Placeholder for Load Code
    }

    public void Settings()
    {
        //Placeholder for Setting Code
    }

    public void MainMenu()
    {
        //Sends player back to the MainMenu
        SceneManager.LoadScene(0);
    }
}

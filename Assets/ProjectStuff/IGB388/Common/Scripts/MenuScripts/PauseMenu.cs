using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public OVRInput.Button pauseButton;
    public GameObject Player;
    public GameObject PauseMenuHolder;

    private void Update()
    {
        PauseMenuHolder.transform.LookAt(Player.transform);

        if (OVRInput.Get(pauseButton)) // Use the appropriate input for Oculus
        {
            Debug.Log("Im here");
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }

}


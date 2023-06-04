using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public OVRInput.Button pauseButton;

    private void Update()
    {

        if (OVRInput.Get(pauseButton)) // Use the appropriate input for Oculus
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.unPauseSFX);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.PauseSFX);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }

}


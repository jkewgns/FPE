using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    bool paused;

    void Start()
    {
        paused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.activeSelf)
                CloseOptions();
            else if (paused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;         // pause the game logic
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        UnlockCursor();
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;         // resume the game
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        LockCursor();
    }

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;         // reset time before changing scene
        SceneManager.LoadScene("Menu");
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

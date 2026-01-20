using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject trackSelectMenu;
    public GameObject optionsMenu;
    public GameObject carSelectMenu;

    void Start()
    {
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        trackSelectMenu.SetActive(false);
        optionsMenu.SetActive(false);
        carSelectMenu.SetActive(false);
    }

    public void OpenTrackSelect()
    {
        mainMenu.SetActive(false);
        trackSelectMenu.SetActive(true);
        optionsMenu.SetActive(false);
        carSelectMenu.SetActive(false);
    }

    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        trackSelectMenu.SetActive(false);
        optionsMenu.SetActive(true);
        carSelectMenu.SetActive(false);
    }

    public void OpenCarSelect()
    {
        mainMenu.SetActive(false);
        trackSelectMenu.SetActive(false);
        optionsMenu.SetActive(false);
        carSelectMenu.SetActive(true);
    }

    public void LoadTrack(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

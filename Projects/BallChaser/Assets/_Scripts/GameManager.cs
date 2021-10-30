using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu, _resume;

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _mainMenu.SetActive(true);
        _resume.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        Screen.orientation = ScreenOrientation.Portrait;
        _mainMenu.SetActive(false);
        _resume.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _mainMenu.SetActive(false);
        _resume.SetActive(false);
    }
}

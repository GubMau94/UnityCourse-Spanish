using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private GameObject _mainPage, _settingPage, _soundPage, _shopPage;

    public static bool joystickLeft = false;

    private Button _buttonChangeLeft, _buttonChangeRight;

    private void Start()
    {
        _buttonChangeLeft = GameObject.Find("LEFT_Button").GetComponent<Button>();
        _buttonChangeRight = GameObject.Find("RIGHT_Button").GetComponent<Button>();

        _mainPage = GameObject.Find("MainPage");
        _settingPage = GameObject.Find("SettingPage");
        _soundPage = GameObject.Find("SoundPage");
        _shopPage = GameObject.Find("ShopPage");

        BeginingSetup();

    }

    private void Update()
    {
        _buttonChangeLeft.gameObject.SetActive(joystickLeft);
        _buttonChangeRight.gameObject.SetActive(!joystickLeft);
    }

    private void BeginingSetup()
    {
        _settingPage.SetActive(false);
        _soundPage.SetActive(false);
        _shopPage.SetActive(false);
        _mainPage.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void JoystickPosition()
    {
        joystickLeft = !joystickLeft;
    }

    public void MainPage()
    {
        _settingPage.SetActive(false);
        _soundPage.SetActive(false);
        _shopPage.SetActive(false);
        _mainPage.SetActive(true);
    }

    public void SettingPage()
    {
        _mainPage.SetActive(false);
        _soundPage.SetActive(false);
        _shopPage.SetActive(false);
        _settingPage.SetActive(true);
    }

}

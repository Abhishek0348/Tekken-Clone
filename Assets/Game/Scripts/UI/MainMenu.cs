using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject SelectCharAndStageMenu;
    public GameObject OptionsMenu;
    public GameObject ControlsMenu;

    private void Start()
    {
        MainMenuUI.SetActive(true);
        SelectCharAndStageMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    public void PlayButtonClicked()
    {
        MainMenuUI.SetActive(false);
        SelectCharAndStageMenu.SetActive(true);
    }

    public void OptionsButtonClicked()
    {
        MainMenuUI.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void ControlsButtonClicked()
    {
        OptionsMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }

    public void BackButtonClicked()
    {
        MainMenuUI.SetActive(true);
        SelectCharAndStageMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    public void SelectCharClicked(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SelectStageClicked(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

}

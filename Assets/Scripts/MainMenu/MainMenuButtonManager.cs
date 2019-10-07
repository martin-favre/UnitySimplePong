using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{

    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene(AvailableScenes.MAIN_GAME_SCENE);
    }

    public void OnOptionsButtonPressed()
    {
        SceneManager.LoadScene(AvailableScenes.OPTIONS_MENU_SCENE);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}

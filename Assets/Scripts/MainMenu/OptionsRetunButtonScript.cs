﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsRetunButtonScript : MonoBehaviour
{
    public void OnReturnButtonPressed()
    {
        SceneManager.LoadScene(AvailableScenes.MAIN_MENU_SCENE);
    }
}

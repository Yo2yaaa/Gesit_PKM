using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadFirstLevelOfJailolo()
    {
        SceneManager.LoadScene("Jailolo_Level 1");
    }
    public void LoadFirstLevelOfBacan()
    {
        SceneManager.LoadScene("Bacan_Level 1");
    }
    public void LoadFirstLevelOfTidore()
    {
        SceneManager.LoadScene("Tidore_Level 1");
    }
    public void LoadFirstLevelOfTernate()
    {
        SceneManager.LoadScene("Ternate_Level 1");
    }
}

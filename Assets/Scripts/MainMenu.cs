using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string midiFileName;
    [SerializeField] private SceneNotExistWindow sceneNotExistWindow;

    private void OpenSceneNotExistWindow(string message)
    {
        sceneNotExistWindow.gameObject.SetActive(true);
        sceneNotExistWindow.messageText.text = message;
    }

    public void PlayTutorial()
    {
        if (Application.CanStreamedLevelBeLoaded("Tutorial"))
        {
            GameData.midiFileName = "Tutorial.mid";  // This File Don't Exist, but we need this parameter
            AnalyticManager.CurrentLevel = 0;
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            OpenSceneNotExistWindow("The Scene doesn't exist");
        }
    }

    public void PlayLevel1()
    {
        if (Application.CanStreamedLevelBeLoaded("FightingScene"))
        {
            GameData.midiFileName = CommonParameter.midiFiles[1];  // Jiacheng
            AnalyticManager.CurrentLevel = 1;
            SceneManager.LoadScene("FightingScene");
        }
        else
        {
            OpenSceneNotExistWindow("The Scene doesn't exist");
        }
    }

    // Jiacheng
    public void PlayLevel2()
    {
        if (Application.CanStreamedLevelBeLoaded("FightingScene"))
        {
            GameData.midiFileName = CommonParameter.midiFiles[2];
            AnalyticManager.CurrentLevel = 2;
            SceneManager.LoadScene("FightingScene");
        }
        else
        {
            OpenSceneNotExistWindow("The Scene doesn't exist");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    // public void PlayTutorial()
    // {
    //     if (Application.CanStreamedLevelBeLoaded("Tutorial"))
    //     {
    //         GameData.midiFileName = CommonParameter.midiFiles[0];  // Jiacheng
    //         AnalyticManager.CurrentLevel = 0;
    //         SceneManager.LoadScene("Tutorial");
    //     }
    //     else
    //     {
    //         OpenSceneNotExistWindow("The Scene doesn't exist");
    //     }
    // }
}
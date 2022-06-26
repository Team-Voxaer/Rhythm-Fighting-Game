using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneNotExistWindow sceneNotExistWindow;

    private void OpenSceneNotExistWindow(string message)
    {
        sceneNotExistWindow.gameObject.SetActive(true);
        sceneNotExistWindow.messageText.text = message;
    }

    public void PlayLevel2()
    {
        if (Application.CanStreamedLevelBeLoaded("Level2"))
        {
            SceneManager.LoadScene("Level2");
        }
        else
        {
            OpenSceneNotExistWindow("Scene Level 2 doesn't exist");
        }
    }

    public void PlayTutorial()
    {
        if (Application.CanStreamedLevelBeLoaded("Tutorial"))
        {
            GameData.midiFileName = "Never_Gonna_Give_You_Up_Kindergarten.mid";  // Jiacheng
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            OpenSceneNotExistWindow("Scene Tutorial doesn't exist");
        }
    }

    public void PlayTutorialPlusPlus()
    {
        if (Application.CanStreamedLevelBeLoaded("Test"))
        {
            SceneManager.LoadScene("Test");
        }
        else
        {
            OpenSceneNotExistWindow("The Scene doesn't exist");
        }
    }

    public void PlayFighting()
    {
        GameData.midiFileName = "Never_Gonna_Give_You_Up.mid";  // Jiacheng
        SceneManager.LoadScene("FightingScene");
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
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
    public void PlayFighting()
    {
        SceneManager.LoadScene("FightingScene");
    }
    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
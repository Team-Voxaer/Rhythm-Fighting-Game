using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Created by Jiacheng
 */

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneNotExistWindow sceneNotExistWindow;
    [SerializeField] private DifficultySelectionWindow difficultySelectionWindow;

    private void OpenSceneNotExistWindow(string message)
    {
        sceneNotExistWindow.gameObject.SetActive(true);
        sceneNotExistWindow.messageText.text = message;
    }

    public void ShowDifficultySelectionWindow()
    {
        difficultySelectionWindow.gameObject.SetActive(true);
        difficultySelectionWindow.messageText.text = string.Join(" ", GameData.songName.Split('_'));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayTutorial()
    {
        if (Application.CanStreamedLevelBeLoaded("Tutorial"))
        {
            AnalyticManager.CurrentLevel = 0;
            GameData.midiFileName = CommonParameter.midiFiles[AnalyticManager.CurrentLevel];
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
            AnalyticManager.CurrentLevel = 1;
            GameData.songName = CommonParameter.midiFiles[AnalyticManager.CurrentLevel];
            ShowDifficultySelectionWindow();
        }
        else
        {
            OpenSceneNotExistWindow("The Scene doesn't exist");
        }
    }

    public void PlayLevel2()
    {
        if (Application.CanStreamedLevelBeLoaded("FightingScene"))
        {
            AnalyticManager.CurrentLevel = 2;
            GameData.songName = CommonParameter.midiFiles[AnalyticManager.CurrentLevel];
            ShowDifficultySelectionWindow();
        }
        else
        {
            OpenSceneNotExistWindow("The Scene doesn't exist");
        }
    }

    public void PlayLevel3()
    {
        if (Application.CanStreamedLevelBeLoaded("FightingScene"))
        {
            AnalyticManager.CurrentLevel = 3;
            GameData.songName = CommonParameter.midiFiles[AnalyticManager.CurrentLevel];
            ShowDifficultySelectionWindow();
        }
        else
        {
            OpenSceneNotExistWindow("The Scene doesn't exist");
        }
    }

    public void PlayLevel4()
    {
        if (Application.CanStreamedLevelBeLoaded("FightingScene"))
        {
            AnalyticManager.CurrentLevel = 4;
            GameData.songName = CommonParameter.midiFiles[AnalyticManager.CurrentLevel];
            ShowDifficultySelectionWindow();
        }
        else
        {
            OpenSceneNotExistWindow("The Scene doesn't exist");
        }
    }

    public void PlayLevel5()
    {
        if (Application.CanStreamedLevelBeLoaded("FightingScene"))
        {
            AnalyticManager.CurrentLevel = 5;
            GameData.songName = CommonParameter.midiFiles[AnalyticManager.CurrentLevel];
            ShowDifficultySelectionWindow();
        }
        else
        {
            OpenSceneNotExistWindow("The Scene doesn't exist");
        }
    }
}
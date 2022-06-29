using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static bool AIEnabled;
    [SerializeField] private EndGameWindow endGameWindow;
    [SerializeField] private DifficultySelectionWindow difficultySelectionWindow;

    private void Start()
    {
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("Menu");
            AnalyticManager.OnLevelEnd("playerQuit"); //Send AnalyticsManager The Game Ended Because Player Quit
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            AnalyticManager.OnLevelEnd("playerReload"); //Send AnalyticsManager The Game Ended Because Player Reload
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            AIEnabled = !AIEnabled;
        }
        // Jiacheng
        // P for pause the game, U for unpause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            SongManager.PauseSong();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SongManager.UnPauseSong();
        }
        // Jiacheng

        // Zhian Li
        if (!SongManager.AudioEnded())
        { 
            EndGameBecauseSongEnded();
        }
    }

    public static bool CheckAI()
    {
        return AIEnabled;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        // int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        // Jiacheng
        if (AnalyticManager.CurrentLevel + 1 < 3 && Application.CanStreamedLevelBeLoaded("FightingScene"))
        {
            AnalyticManager.CurrentLevel += 1;
            GameData.songName = CommonParameter.midiFiles[AnalyticManager.CurrentLevel];
            ShowDifficultySelectionWindow();
        }
        else
        {
            ReturnToMenu();
        }
    }

    public void ShowDifficultySelectionWindow()
    {
        difficultySelectionWindow.gameObject.SetActive(true);
        difficultySelectionWindow.messageText.text = string.Join(" ", GameData.songName.Split('_'));
    }

    public void EndGame(string name)
    {
        SongManager.PauseSong();
        endGameWindow.gameObject.SetActive(true);
        endGameWindow.messageText.text = $"Congratulation, {name} Win !!\nLevel " + AnalyticManager.CurrentLevel + "  " + GameData.difficulty + 
                                          "\n" + string.Join(" ", GameData.songName.Split('_'));
        AnalyticManager.OnLevelEnd("playerDied"); //Send AnalyticsManager The Game Ended Because The Player Died
    }

    public void EndGameBecauseSongEnded()
    {
        endGameWindow.gameObject.SetActive(true);
        endGameWindow.messageText.text = $"The Song Ended.\nLevel " + AnalyticManager.CurrentLevel + ", " + GameData.difficulty + 
                                          "\n" + string.Join(" ", GameData.songName.Split('_'));
        AnalyticManager.OnLevelEnd("songEnded"); //Send AnalyticsManager The Game Ended Because Song Ended
    }
}

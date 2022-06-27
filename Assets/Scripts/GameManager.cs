using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static bool AIEnabled;
    [SerializeField] private EndGameWindow endGameWindow;

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
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public static void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (Application.CanStreamedLevelBeLoaded(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            ReturnToMenu();
        }
    }

    public void EndGame(string name)
    {
        endGameWindow.gameObject.SetActive(true);
        endGameWindow.messageText.text = $"Congratulation, {name} Win !!";
        AnalyticManager.OnLevelEnd(false); //Send AnalyticsManager The Game Ended Because Song Ended
    }

    public void EndGameBecauseSongEnded()
    {
        endGameWindow.gameObject.SetActive(true);
        endGameWindow.messageText.text = $"The Song Ended";
        AnalyticManager.OnLevelEnd(true); //Send AnalyticsManager The Game Ended Because Song Ended
    }
}

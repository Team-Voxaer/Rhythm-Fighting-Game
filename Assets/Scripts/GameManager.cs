using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    public static GameManager gameManager;
    public static bool AIEnabled;
    [SerializeField] private EndGameWindow endGameWindow;

    private void Start()
    {
        gameManager = this;
    }

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
    }
}

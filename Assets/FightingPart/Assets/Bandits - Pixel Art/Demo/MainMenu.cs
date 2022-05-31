using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayRhythm()
    {
        SceneManager.LoadScene("SampleScene");
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
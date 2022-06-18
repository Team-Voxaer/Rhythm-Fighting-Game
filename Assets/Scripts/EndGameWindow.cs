using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameWindow : MonoBehaviour
{
    public Button returnButton;
    public Button NextButton;
    public TextMeshProUGUI messageText;

    public void ReturnCliked()
    {
        GameManager.ReturnToMenu();
    }

    public void NextClicked()
    {
        GameManager.LoadNextScene();
    }
}

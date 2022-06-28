using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialEndWindow : MonoBehaviour
{
    public Button returnButton;
    public Button restartButton;
    public TextMeshProUGUI messageText;

    public void ReturnCliked()
    {
        GameManager.ReturnToMenu();
    }

    public void RestartClicked()
    {
        GameManager.ReloadScene();
    }
}

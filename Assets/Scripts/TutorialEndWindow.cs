using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialEndWindow : MonoBehaviour
{
    public Button returnButton;
    public Button restartButton;
    public TextMeshProUGUI messageText;

    public void ReturnCliked()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

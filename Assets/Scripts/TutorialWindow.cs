using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * Created by Jiacheng
 */

public class TutorialWindow : MonoBehaviour
{
    public Button closeButton;
    public TextMeshProUGUI messageText;
    public Image image;

    public void CloseClicked()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseClicked();
        }
    }
}

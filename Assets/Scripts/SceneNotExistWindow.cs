using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneNotExistWindow : MonoBehaviour
{
    public Button closeButton;
    public TextMeshProUGUI messageText;

    public void CloseClicked()
    {
        this.gameObject.SetActive(false);
    }
}

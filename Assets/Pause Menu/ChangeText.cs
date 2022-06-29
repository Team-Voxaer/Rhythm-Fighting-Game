using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchAIMode()
    {
        if (GameData.AILevel == 0) {
            buttonText.text = "Easy AI";
            GameData.AILevel = 1;
        } else if (GameData.AILevel == 1) {
            buttonText.text = "Medium AI";
            GameData.AILevel = 2;
        } else if (GameData.AILevel == 2) {
            buttonText.text = "Hard AI";
            GameData.AILevel = 3;
        } else if (GameData.AILevel == 3) {
            buttonText.text = "AI Disabled";
            GameData.AILevel = 0;
        }      
    }
}

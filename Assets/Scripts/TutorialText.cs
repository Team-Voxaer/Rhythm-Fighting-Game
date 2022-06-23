using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public TextMeshProUGUI textInfo;
    private List<string> textInfos = new List<string>()
    {
        "Hit the Notes when it reachs White Bar with elements \n Left Player use WASD \n Right Player use Arrow Keys",
        "Press W or ↑ three times in a row to use MAGIC attack element Thunder",
        "Press D or → three times in a row to use strong PHYSICAL attack with element Fire",
        "Press A or ← three times in a row to use shield with element Light \n Shield Can only guard PHYSICAL Damage",
        "Press S or ↓ three times in a row to use healing with element Earth",
        "You are good to go now, GLHF"
    };

    private List<int> skillUseNumber = new List<int>()
    {
        2, 4, 4, 4, 4, 4, 4, 4, 4
    };


    public List<int> lsSkill = new List<int>();

    private int currentTextIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void countdown(int index)
    {
        if (currentTextIndex == index) skillUseNumber[index]--;
        UpdateText();
    }

    void UpdateText()
    {
        if (currentTextIndex == textInfos.Count) return; // reached the last one
        if (skillUseNumber[currentTextIndex] == 0){
            currentTextIndex++;
        }

        if (currentTextIndex == textInfos.Count - 1){
            textInfo.text = textInfos[currentTextIndex];
        }
        else if (currentTextIndex != 0) {
            textInfo.text = textInfos[currentTextIndex] + "\n You need to use it " + skillUseNumber[currentTextIndex].ToString() + " times";
        }

    }
}

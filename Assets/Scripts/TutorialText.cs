using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public TextMeshProUGUI textInfo;
    private List<string> textInfos = new List<string>()
    {
        "Hit the Notes when power elements reach the ground \n Left Player uses WASD \n Right Player uses Arrow Keys",
        "Press W (Player 1) or Up Arrow (Player 2) three times to use MAGIC attack, Thunder",
        "Press D (Player 1) or Right Arrow (Player 2) three times to use PHYSICAL attack, Fire",
        "Press A (Player 1) or Left Arrow (Player 2) three times to shield youself with Light \n Shield only prevents PHYSICAL Damage",
        "Press S (Player 1) or Right Arrow (Player 2) three times to heal youself with Earth",
        "Are You Ready Now? Good Luck Have Fun:)"
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

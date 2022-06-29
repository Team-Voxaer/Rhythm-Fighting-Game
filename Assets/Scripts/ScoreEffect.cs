using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rhythm;
using TMPro;
public class ScoreEffect : MonoBehaviour
{
    public GameObject scoreTextPrefeb, playerInstance;
    public string textToDisplay;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            GameObject ScoreText = Instantiate(scoreTextPrefeb, playerInstance.transform);
            //ScoreText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);
            ScoreText.transform.GetChild(0).GetComponent<TextMeshPro>().text = "TESTTEXT";
        }
    }    
    
    // //ScoreEffect    
    public void ScoreFeedback(HitLevel hitLevel){
        GameObject scoreText = Instantiate(scoreTextPrefeb, playerInstance.transform);

        if(hitLevel == Rhythm.HitLevel.Perfect){
            textToDisplay = "Perfect!!";
            scoreText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);    
        }
        else if(hitLevel == Rhythm.HitLevel.Good){
            textToDisplay = "Good!";
            scoreText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);    
        }
        else if(hitLevel == Rhythm.HitLevel.Bad){
            textToDisplay = "Not Bad";
            scoreText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);    
        }
        else{
            print("Miss");
        }
    }

    // public void printScore(Rhythm.HitLevel hitLevel){
    //     print("SCOREEFFECT METHOD TEST!!!!" + hitLevel);
    // }       


}

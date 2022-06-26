using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rhythm;

public class ScoreEffect : MonoBehaviour
{
    public GameObject floatingPoints;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(floatingPoints, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //ScoreEffect
    public void ScoreFeedback(HitLevel hitLevel){

        if(hitLevel == Rhythm.HitLevel.Perfect){
            Instantiate(floatingPoints, transform.position, Quaternion.identity);
        }
        else if(hitLevel == Rhythm.HitLevel.Good){
            Instantiate(floatingPoints, transform.position, Quaternion.identity);
        }
        else if(hitLevel == Rhythm.HitLevel.Bad){
            Instantiate(floatingPoints, transform.position, Quaternion.identity);
        }
        else{
            Instantiate(floatingPoints, transform.position, Quaternion.identity);
        }
}
}

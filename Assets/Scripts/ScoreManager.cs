using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TextMeshPro scoreText;
    int comboScore;

    // Start is called before the first frame update
    void Start()
    {
        comboScore = 0;
    }

    public void Hit()
    {
        comboScore += 1;
    }
    public void Miss()
    {
        comboScore -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = comboScore.ToString();
    }

    
}

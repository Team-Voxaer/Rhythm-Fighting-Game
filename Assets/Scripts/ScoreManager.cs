using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Bandit bandit;
    int comboScore;

    
    // List of keys for skills
    List<int> lsKey = new List<int>();
    List<int> lsSkill = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        comboScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = comboScore.ToString();
    }

    public void Hit(int inputKey) {
        lsKey.Add(inputKey);

        comboScore += 1;
        bandit.Attack();
    }
    
    public void Miss()
    {
        comboScore -= 1;
    }

    public void CastSkill() {
        CheckSkills();
        if (lsSkill.Count > 0) {
            bandit.Attack();
            lsSkill.RemoveAt(0);
        }
    }

    private void CheckSkills() {
        while (lsKey.Count > 3) {
            lsKey.RemoveAt(0);
        }
        if (lsKey[-1] == 3 && lsKey[-2] == 1 && lsKey[-1] == 1)  // Down Down Right
        {
            lsSkill.Add(0);
            for (int i=0; i<3; i++) {
                lsKey.RemoveAt(-1);
            }
        }
    }
    
}

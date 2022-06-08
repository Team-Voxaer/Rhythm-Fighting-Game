using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Bandit bandit;
    int comboScore;

    private int skillCodeDefend = 0;
    private int skillCodeAttack = 1;

    
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

        comboScore += 10;
        // bandit.Attack();
    }
    
    public void Miss()
    {
        comboScore -= 1;
    }

    public void CastSkill() {
        CheckSkills();
        if (lsSkill.Count > 0) {
            if (lsSkill[0] == skillCodeDefend) {
                bandit.Defend();
            } else if (lsSkill[0] == skillCodeAttack) {
                bandit.Attack();
            }
            lsSkill.RemoveAt(0);
        } else {
            // TODO: a visualization for no skills when casting
        }
    }

    private void CheckSkills() {
        while (lsKey.Count > 3) {
            lsKey.RemoveAt(0);
        }
        // 0: up
        // 1: down
        // 2: left
        // 3: right
        if (lsKey[lsKey.Count - 1] == 3 && lsKey[lsKey.Count - 2] == 0 && lsKey[lsKey.Count - 3] == 0)  // Up Up Right
        {
            lsSkill.Add(skillCodeAttack);
            for (int i=0; i<3; i++) {
                lsKey.RemoveAt(0);
            }
        } else if (lsKey[lsKey.Count - 1] == 1 && lsKey[lsKey.Count - 2] == 1 && lsKey[lsKey.Count - 3] == 1)  // Down Down Down
        {
            lsSkill.Add(skillCodeDefend);
            for (int i=0; i<3; i++) {
                lsKey.RemoveAt(0);
            }
        }
    }
    
}

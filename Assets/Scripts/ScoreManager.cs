using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public PlayerController player;
    int comboScore;

    public KeyCode inputKeyCast;

    private int skillCodeDefend = 0;
    private int skillCodeAttack = 1;

    
    // List of keys for skills
    List<int> lsKey = new List<int>();
    List<int> lsSkill = new List<int>();

    // public GameObject visCombo1;
    // public GameObject visCombo2;
    // public GameObject visCombo3;
    public VisCombo visCombo1;
    public VisCombo visCombo2;
    public VisCombo visCombo3;

    public Sprite imgComboUp, imgComboDown, imgComboLeft, imgComboRight;
    

    // Start is called before the first frame update
    void Start()
    {
        comboScore = 0;
        // imgComboUp = Sprite.Create(Resources.Load<Texture2D>("Assets/Sprites/up.png"), new Rect(0, 0, 20, 20), new Vector2(0.2f, 0.5f));
        // imgComboDown = Resources.Load<Sprite>("Assets/Sprites/down.png");
        // imgComboLeft = Resources.Load<Sprite>("left");
        // imgComboRight = Resources.Load<Sprite>("right");
        // visCombo1.GetComponent<Image>();
        // visCombo2.GetComponent<Image>();
        // visCombo3.GetComponent<Image>();
        // imgComboRight = Resources.Load<Sprite>("Assets/image/Arrows/right");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputKeyCast)) {
            CastSkill();
        }

        scoreText.text = "Point: " + comboScore.ToString();

        if (lsKey.Count == 0) {
            VisCombo(visCombo1, -1);
            VisCombo(visCombo2, -1);
            VisCombo(visCombo3, -1);
        } else {
            if (lsKey.Count >= 1) {
                VisCombo(visCombo1, lsKey[0]);
                // visCombo1.SetActive(true);
            }
            if (lsKey.Count == 2) {
                VisCombo(visCombo1, lsKey[0]);
                VisCombo(visCombo2, lsKey[1]);
                // visCombo2.SetActive(true);
            }
            if (lsKey.Count >= 3) {
                VisCombo(visCombo3, lsKey[lsKey.Count - 1]);
                VisCombo(visCombo2, lsKey[lsKey.Count - 2]);
                VisCombo(visCombo1, lsKey[lsKey.Count - 3]);
                // visCombo3.SetActive(true);
            }
        }
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
                player.Defend();
            } else if (lsSkill[0] == skillCodeAttack) {
                player.Attack();
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
        if (lsKey.Count == 3 && lsKey[lsKey.Count - 1] == 3 && lsKey[lsKey.Count - 2] == 0 && lsKey[lsKey.Count - 3] == 0)  // Up Up Right
        {
            lsSkill.Add(skillCodeAttack);
            for (int i=0; i<3; i++) {
                lsKey.RemoveAt(0);
            }
        } else if (lsKey.Count == 3 && lsKey[lsKey.Count - 1] == 1 && lsKey[lsKey.Count - 2] == 1 && lsKey[lsKey.Count - 3] == 1)  // Down Down Down
        {
            lsSkill.Add(skillCodeDefend);
            for (int i=0; i<3; i++) {
                lsKey.RemoveAt(0);
            }
        }
    }

    private void VisCombo(VisCombo visComboN, int idxKey) {
        if (idxKey == 0) {
            // visComboN = GetComponent<Image>();
            visComboN.changeImage(imgComboUp);
        } else if (idxKey == 1) {
            // visComboN.GetComponent<Image>().overrideSprite = imgComboDown;
            visComboN.changeImage(imgComboDown);
            // visComboN.sprite = imgComboDown;
        } else if (idxKey == 2) {
            // visComboN.GetComponent<Image>().overrideSprite = imgComboLeft;
            visComboN.changeImage(imgComboLeft);

            // visComboN.sprite = imgComboLeft;
        } else if (idxKey == 3) {
            // visComboN.GetComponent<Image>().overrideSprite = imgComboRight;
            visComboN.changeImage(imgComboRight);

            // visComboN.sprite = imgComboRight;
        } else if (idxKey == -1) {
            visComboN.changeImage(null);
        }
        // visComboN.SetActive(true);
    }
    
}

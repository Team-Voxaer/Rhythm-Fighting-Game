using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Rhythm;

public class ScoreManager : MonoBehaviour
{
    // public TextMeshProUGUI scoreText;
    public PlayerController player;
    int comboScore;

    public KeyCode inputKeyCast;

    private const int skillCodeDefend = 0;
    private const int skillCodeAttack = 1;
    private const int skillCodeSword = 2;
    private const int skillCodeGrandCross = 3;
    private const int skillCodeThunder = 4;

    // List of keys for skills
    List<Direction> lsKey = new List<Direction>();
    List<int> lsSkill = new List<int>();

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
        // if (Input.GetKeyDown(inputKeyCast)) {
        //     CastSkill();
        // }
        // CastSkill();

        // scoreText.text = "Point: " + comboScore.ToString();

        VisualizeCombo();
    }

    public void Hit(Direction direction, HitLevel hitLevel, int index) {
        lsKey.Add(direction);

        comboScore += 10;
        
        VisualizeCombo();
        CastSkill();
    }
    
    public void Miss(int index)
    {
        comboScore -= 1;
        lsKey.Clear();
    }

    public void CastSkill() {
        CheckSkills();
        if (lsSkill.Count > 0) {
            if (lsSkill[0] == skillCodeDefend) { // Todo: Change to heal
                player.Defend(); 
                // player.Heal();
            } else if (lsSkill[0] == skillCodeAttack) {
                player.Attack();
            } else if (lsSkill[0] == skillCodeSword) {
                player.UseSword();
            } else if (lsSkill[0] == skillCodeGrandCross) {
                player.UseGrandCross();
            } else if (lsSkill[0] == skillCodeThunder) {
                player.UseThunder();
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
        if (lsKey.Count == 3) {
            if (lsKey[lsKey.Count - 1] == Direction.Up && lsKey[lsKey.Count - 2] == Direction.Up && lsKey[lsKey.Count - 3] == Direction.Up)  // Up Up Up -> Thunder / Water
            {
                lsSkill.Add(skillCodeThunder);
            } 
            else if (lsKey[lsKey.Count - 1] == Direction.Left && lsKey[lsKey.Count - 2] == Direction.Left && lsKey[lsKey.Count - 3] == Direction.Left)  // Left Left Left -> Light
            {
                lsSkill.Add(skillCodeGrandCross);
            } 
            else if (lsKey[lsKey.Count - 1] == Direction.Down && lsKey[lsKey.Count - 2] == Direction.Down && lsKey[lsKey.Count - 3] == Direction.Down) // Down Down Down -> Earth 
            {
                lsSkill.Add(skillCodeDefend);
            } 
            else if (lsKey[lsKey.Count - 1] == Direction.Right && lsKey[lsKey.Count - 2] == Direction.Right && lsKey[lsKey.Count - 3] == Direction.Right) // Right Right Right -> Fire
            {
                lsSkill.Add(skillCodeSword);
            } 
            else
            {
                lsSkill.Add(skillCodeAttack);
            }
            for (int i=0; i<3; i++) {
                lsKey.Clear();
            }
        }
    }

    private void VisualizeCombo() {
        if (lsKey.Count == 0) {
            VisCombo(visCombo1, Direction.None);
            VisCombo(visCombo2, Direction.None);
            VisCombo(visCombo3, Direction.None);
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
    private void VisCombo(VisCombo visComboN, Direction direction) {
        if (direction == Direction.Up) {
            // visComboN = GetComponent<Image>();
            visComboN.changeImage(imgComboUp);
        } else if (direction == Direction.Down) {
            // visComboN.GetComponent<Image>().overrideSprite = imgComboDown;
            visComboN.changeImage(imgComboDown);
            // visComboN.sprite = imgComboDown;
        } else if (direction == Direction.Left) {
            // visComboN.GetComponent<Image>().overrideSprite = imgComboLeft;
            visComboN.changeImage(imgComboLeft);

            // visComboN.sprite = imgComboLeft;
        } else if (direction == Direction.Right) {
            // visComboN.GetComponent<Image>().overrideSprite = imgComboRight;
            visComboN.changeImage(imgComboRight);

            // visComboN.sprite = imgComboRight;
        } else if (direction == Direction.None) {
            visComboN.changeImage(null);
        }
        // visComboN.SetActive(true);
    }
    
}

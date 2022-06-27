using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject dropPoint;
    public GameObject tutorialNote;
    public ScoreManager scoreManager;
    // Check it player hit keys we want
    static string hitNoteFeedBack;
    public PlayerController player1;
    public PlayerController player2;
    public GameObject jumpingText;

    [SerializeField] private TutorialWindow tutorialWindow;  // Jiacheng

    bool WWWSkill = false;
    bool AAASkill = false;
    bool SSSSkill = false;
    bool DDDSkill = false;

    void Start()
    {
        hitNoteFeedBack = "";
        jumpingText.SetActive(false);
        StartCoroutine(WorkFlow());
        player1.SetTutorial();
        player2.SetTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitNoteFeedBack.Length > 3)
        {
            hitNoteFeedBack = "";
        }
        if (hitNoteFeedBack == "DDD")
        {
            DDDSkill = true;
            hitNoteFeedBack = "";
            AAASkill = false;
            SSSSkill = false;
            WWWSkill = false;
        }
        else if (hitNoteFeedBack == "AAA")
        {
            AAASkill = true;
            hitNoteFeedBack = "";
            SSSSkill = false;
            DDDSkill = false;
            WWWSkill = false;
        }
        else if (hitNoteFeedBack == "WWW")
        {
            WWWSkill = true;
            hitNoteFeedBack = "";
            SSSSkill = false;
            AAASkill = false;
            DDDSkill = false;
        }
        else if (hitNoteFeedBack == "SSS")
        {
            SSSSkill = true;
            hitNoteFeedBack = "";
            AAASkill = false;
            WWWSkill = false;
            WWWSkill = false;
        }
    }

    void InstantiateNote()
    {
        GameObject note = Instantiate(tutorialNote, dropPoint.transform.position, Quaternion.identity);
        note.GetComponent<TutorialNote>().SetTutorialScript(this);
    }


    public void GetHitFeedBack(char c)
    {
        hitNoteFeedBack += c;
        if (c == 'W')
        {
            scoreManager.Hit(Rhythm.Direction.Up, Rhythm.HitLevel.Perfect, 0);
        }
        else if (c == 'A')
        {
            scoreManager.Hit(Rhythm.Direction.Left, Rhythm.HitLevel.Perfect, 0);
        }
        else if (c == 'S')
        {
            scoreManager.Hit(Rhythm.Direction.Down, Rhythm.HitLevel.Perfect, 0);
        }
        else if (c == 'D')
        {
            scoreManager.Hit(Rhythm.Direction.Right, Rhythm.HitLevel.Perfect, 0);
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator WorkFlow()
    {
        ShowTutorialWindow("Press < D > when a white block hits the ground to gain one stack of physical attack energy, " + 
                           "consume three consecutive < D > stacks to cast physical attack on your opponent.", "Image/1");  // Jiacheng

        jumpingText.GetComponent<TextMeshPro>().text = "D";
        jumpingText.SetActive(true);

        while (!DDDSkill)
        {
            InstantiateNote();
            yield return new WaitForSeconds(1.6f);
        }
        ShowTutorialWindow("Press < A > when a white block hits the ground to gain one stack of defend energy, " + 
                           "consume three consecutive < A > stacks to defend physical attack from your opponent.", "Image/2");  // Jiacheng

        jumpingText.GetComponent<TextMeshPro>().text = "A";
        while (!AAASkill)
        {
            player2.UseSword();
            InstantiateNote();
            yield return new WaitForSeconds(1.6f);
        }
        player2.UseSword();
        yield return new WaitForSeconds(1.6f);
        player2.UseSword();
        yield return new WaitForSeconds(1.6f);

        ShowTutorialWindow("Press < W > when a white block hits the ground to gain one stack of magical attack energy, " + 
                           "consume three consecutive < W > stacks to cast magical attack that penetrate opponent's defense.", "Image/3");  // Jiacheng

        jumpingText.GetComponent<TextMeshPro>().text = "W";
        while (!WWWSkill)
        {
            player2.UseGrandCross();
            InstantiateNote();
            yield return new WaitForSeconds(1.6f);
        }
        player2.UseHealing();
        ShowTutorialWindow("Press < S > when a white block hits the ground to gain one stack of heal energy, " + 
                           "consume three consecutive < S > stacks to heal your character.", "Image/4");  // Jiacheng

        jumpingText.GetComponent<TextMeshPro>().text = "S";
        while (!SSSSkill)
        {
            InstantiateNote();
            yield return new WaitForSeconds(1.6f);
        }
        jumpingText.SetActive(false);
        ShowTutorialWindow("Tutorial ends.", "Image/5");  // Jiacheng
        yield break;
    }

    // Jiacheng
    private void ShowTutorialWindow(string text, string imagePath)
    {
        tutorialWindow.gameObject.SetActive(true);
        tutorialWindow.messageText.text = text;
        tutorialWindow.image.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
    }
}

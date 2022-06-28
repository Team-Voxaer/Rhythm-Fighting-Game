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
    [SerializeField] private TutorialEndWindow tutorialEndWindow;  // Jiacheng

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
        else if (hitNoteFeedBack.Length >= 3)
        {
            hitNoteFeedBack = "";
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
        // Jiacheng
        ShowTutorialWindow("Catch the white block when it hits the ground. (Close this window first)", "Image/0");
        while (tutorialWindow.gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(1.6f);
        }
        ShowTutorialWindow("Physical attack deals 50 damage. (Close this window first)", "Image/1");
        // Jiacheng

        jumpingText.GetComponent<TextMeshPro>().text = "D";
        jumpingText.SetActive(true);

        while (!DDDSkill)
        {
            InstantiateNote();
            yield return new WaitForSeconds(1.6f);
        }
        ShowTutorialWindow("Shield can reduce physical attack damage to 1. (Close this window first)", "Image/2");  // Jiacheng

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

        ShowTutorialWindow("Magical attack can penetrate shield and deals 40 damage. (Close this window first)", "Image/3");  // Jiacheng

        jumpingText.GetComponent<TextMeshPro>().text = "W";
        while (!WWWSkill)
        {
            player2.UseGrandCross();
            InstantiateNote();
            yield return new WaitForSeconds(1.6f);
        }
        player2.UseHealing();
        ShowTutorialWindow("Heal spell recovers 30 health. (Close this window first)", "Image/4");  // Jiacheng

        jumpingText.GetComponent<TextMeshPro>().text = "S";
        while (!SSSSkill)
        {
            InstantiateNote();
            yield return new WaitForSeconds(1.6f);
        }
        jumpingText.SetActive(false);
        tutorialWindow.gameObject.SetActive(false);  // Jiacheng
        ShowTutorialEndWindow();  // Jiacheng
        yield break;
    }

    // Jiacheng
    private void ShowTutorialWindow(string text, string imagePath)
    {
        tutorialWindow.gameObject.SetActive(true);
        tutorialWindow.messageText.text = text;
        tutorialWindow.image.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
    }

    // Jiacheng
    private void ShowTutorialEndWindow()
    {
        tutorialEndWindow.gameObject.SetActive(true);
    }
}

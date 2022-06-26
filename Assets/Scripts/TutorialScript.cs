using System.Collections;
using System.Collections.Generic;
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

    bool WWWSkill = false;
    bool AAASkill = false;
    bool SSSSkill = false;
    bool DDDSkill = false;

    void Start()
    {
        hitNoteFeedBack = "";
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
        // TODO: Pop a window and end tutorial or do it in the flow
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
        // TODO: Pop a window
        while (!DDDSkill)
        {
            Instantiate(tutorialNote, dropPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.6f);
        }
        // TODO: Pop a window
        while (!AAASkill)
        {
            player2.UseSword();
            Instantiate(tutorialNote, dropPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.6f);
        }
        player2.UseSword();
        yield return new WaitForSeconds(1.6f);
        // TODO: Pop a window
        while (!WWWSkill)
        {
            player2.UseGrandCross();
            Instantiate(tutorialNote, dropPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.6f);
        }
        player2.UseHealing();
        // TODO: Pop a window
        while (!SSSSkill)
        {
            Instantiate(tutorialNote, dropPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.6f);
        }
        // TODO: Pop a window
        print("reached the end");
        yield break;



    }
}

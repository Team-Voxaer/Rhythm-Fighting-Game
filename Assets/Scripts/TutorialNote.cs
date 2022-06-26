using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNote : MonoBehaviour
{
    public float speed = 1f;
    private bool triggerStay = false;
    public TutorialScript tutorialScript;
    // positive value move upward, negative value move downward
    public float moveDirection = -1f;

    private void Start()
    {
        tutorialScript = GameObject.Find("TutorialScript").GetComponent<TutorialScript>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.W) && triggerStay)
        {
            // print("hit with W");
            tutorialScript.GetHitFeedBack('W');
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.A) && triggerStay)
        {
            // print("hit with A");
            tutorialScript.GetHitFeedBack('A');
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.S) && triggerStay)
        {
            // print("hit with S");
            tutorialScript.GetHitFeedBack('S');
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.D) && triggerStay)
        {
            // print("hit with D");
            tutorialScript.GetHitFeedBack('D');
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0f, moveDirection, 0f), Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerStay = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}

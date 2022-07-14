using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject creditsText;
    bool destroyed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Menu");
        }

        creditsText = GameObject.Find("CreditsText");
        // print(creditsText);
        if(destroyed == true){
            print("creditsText is null now");
            SceneManager.LoadScene("Menu");
        }
        
    }

    public void DestroyCredits(){
        //Destroy(gameObject.transform.parent.gameObject);
        destroyed = true;
        Destroy(gameObject);
        
    }
}

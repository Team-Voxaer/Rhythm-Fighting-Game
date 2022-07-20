using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource audioSource;  // to play audio, imported from UnityEngine
    public GameObject creditsText;
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

        if(creditsText.transform.localPosition.y > 1320.0){
            print("creditsText is null now");
            SceneManager.LoadScene("Menu");
        }
        
    }

    public void LoadMainmenu() {
        SceneManager.LoadScene("Menu");
    }
}

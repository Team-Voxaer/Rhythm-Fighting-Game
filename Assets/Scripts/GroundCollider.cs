using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("TutorialNote"))
        {
            /*print($"name of collider is {collider}");*/
            audioSource.PlayOneShot(clip, 1f);
        }
    }

}

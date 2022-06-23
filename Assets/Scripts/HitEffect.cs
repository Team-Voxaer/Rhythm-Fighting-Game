using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    Renderer rend;
    int effectTimer;
    // Start is called before the first frame update
    void Start()
    {
        // Try Not Hardcode Y Position
        // transform.localPosition = Vector3.up * SongManager.Instance.noteSpawnY;
        effectTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (effectTimer > 0){
            effectTimer--;
        }
        else {
            rend = GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.white);
        }
    }

    public void ChangeColor(int inputKey){
        rend = GetComponent<Renderer>();
        Color c0 = Color.magenta;
        Color c1 = Color.green;
        Color c2 = Color.blue;
        Color c3 = Color.black;
        // Up0 Down1 Left2 Right3
        if(inputKey == 0){
            rend.material.SetColor("_Color", c0);          
        }
        else if(inputKey == 1){
            rend.material.SetColor("_Color", c1);    
        }
        else if(inputKey == 2){
            rend.material.SetColor("_Color", c2);    
        }
        else{
            rend.material.SetColor("_Color", c3);    
        }      
        
        effectTimer = 15;
        
        
    }
}

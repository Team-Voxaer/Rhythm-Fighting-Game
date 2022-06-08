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

    public void ChangeColor(){
        rend = GetComponent<Renderer>();
        
        rend.material.SetColor("_Color", Color.black);
        effectTimer = 10;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rhythm;
public class HitEffect : MonoBehaviour
{
    Renderer rend;
    int effectTimer;

    //Image appears when key's hit
    public Image oldImg;
    public Sprite spriteA;
    public Sprite spriteW;
    public Sprite spriteS;
    public Sprite spriteD;
    public Sprite spriteOrigin;

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

    /*
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    public enum HitLevel
    {
        Perfect,
        Good,
        Bad,
        Invalid
    }
    */    
    // public void ShowHitKey(Direction direction, HitLevel hitLevel){
    //     spriteOrigin = oldImg.sprite;
    //     if(direction == Rhythm.Direction.Up){
    //         if(hitLevel == HitLevel.Perfect){ShowImg(spriteW);}
    //         else if (hitLevel == HitLevel.Good){ShowImg(spriteW);}
    //         else if (hitLevel == HitLevel.Bad){ShowImg(spriteW);}
    //         else {ShowImg(spriteOrigin);}
    //     }
    //     else if(direction == Rhythm.Direction.Down){
    //         if(hitLevel == HitLevel.Perfect){ShowImg(spriteS);}
    //         else if (hitLevel == HitLevel.Good){ShowImg(spriteS);}
    //         else if (hitLevel == HitLevel.Bad){ShowImg(spriteS);}
    //         else {ShowImg(spriteOrigin);}
    //     }
    //     else if(direction == Rhythm.Direction.Left){
    //         if(hitLevel == HitLevel.Perfect){ShowImg(spriteA);}
    //         else if (hitLevel == HitLevel.Good){ShowImg(spriteA);}
    //         else if (hitLevel == HitLevel.Bad){ShowImg(spriteA);}
    //         else {ShowImg(spriteOrigin);}
    //     }
    //     else if(direction == Rhythm.Direction.Right){
    //         if(hitLevel == HitLevel.Perfect){ShowImg(spriteD);}
    //         else if (hitLevel == HitLevel.Good){ShowImg(spriteD);}
    //         else if (hitLevel == HitLevel.Bad){ShowImg(spriteD);}
    //         else {ShowImg(spriteOrigin);}            
    //     }
    //     else{
    //         //Not hit, shows nothing
    //         ShowImg(spriteOrigin);
    //     }
    // }

    // void ShowImg(Sprite newSprite){
    //     oldImg.sprite = newSprite; 
    // }
}

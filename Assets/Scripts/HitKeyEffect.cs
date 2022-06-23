using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rhythm;
public class HitKeyEffect : MonoBehaviour
{

     //Image appears when key's hit
    public Image oldImg;
    public Sprite spriteA;
    public Sprite spriteW;
    public Sprite spriteS;
    public Sprite spriteD;
    public Sprite spriteOrigin;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void ShowHitKey(Direction direction, HitLevel hitLevel){
        spriteOrigin = oldImg.sprite;
        if(direction == Rhythm.Direction.Up){
            if(hitLevel == HitLevel.Perfect){ShowImg(spriteW);}
            else if (hitLevel == HitLevel.Good){ShowImg(spriteW);}
            else if (hitLevel == HitLevel.Bad){ShowImg(spriteW);}
            else {ShowImg(spriteOrigin);}
        }
        else if(direction == Rhythm.Direction.Down){
            if(hitLevel == HitLevel.Perfect){ShowImg(spriteS);}
            else if (hitLevel == HitLevel.Good){ShowImg(spriteS);}
            else if (hitLevel == HitLevel.Bad){ShowImg(spriteS);}
            else {ShowImg(spriteOrigin);}
        }
        else if(direction == Rhythm.Direction.Left){
            if(hitLevel == HitLevel.Perfect){ShowImg(spriteA);}
            else if (hitLevel == HitLevel.Good){ShowImg(spriteA);}
            else if (hitLevel == HitLevel.Bad){ShowImg(spriteA);}
            else {ShowImg(spriteOrigin);}
        }
        else if(direction == Rhythm.Direction.Right){
            if(hitLevel == HitLevel.Perfect){ShowImg(spriteD);}
            else if (hitLevel == HitLevel.Good){ShowImg(spriteD);}
            else if (hitLevel == HitLevel.Bad){ShowImg(spriteD);}
            else {ShowImg(spriteOrigin);}            
        }
        else{
            //Not hit, shows nothing
            ShowImg(spriteOrigin);
        }
    }

    void ShowImg(Sprite newSprite){
        oldImg.sprite = newSprite; 
    }
}

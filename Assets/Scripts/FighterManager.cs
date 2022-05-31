using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Fighter;

public class FighterManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Fighter playerLeft = new Fighter(true);
        Fighter playerRight = new Fighter(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

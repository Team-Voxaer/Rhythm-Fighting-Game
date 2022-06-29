using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyParent(){
        //Destroy(gameObject.transform.parent.gameObject);
        Destroy(gameObject);
    }
}

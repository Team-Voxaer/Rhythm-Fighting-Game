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
        transform.localPosition += new Vector3(-0.001f, 0.04f, 0);
    }
    public void DestroyParent(){
        //Destroy(gameObject.transform.parent.gameObject);
        
        Destroy(gameObject);
        
    }
}

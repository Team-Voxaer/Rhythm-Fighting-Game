using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSkill : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }

}

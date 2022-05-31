using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int curHealth;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    public void TakenDamage(int damage)
    {
        curHealth -= damage;
        animator.SetTrigger("Hurt");
        if (curHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Death");
        Debug.Log("Enemy died");
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

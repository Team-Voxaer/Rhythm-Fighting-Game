using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator animator;
    public float attackRange = 5;
    private LayerMask enemyLayers;
    private double ratio = 1;
    public enum SKILL
    {
        sword,
        beam,
        Thunder

    }
    public SKILL skillName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Attack();
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

    public void SetEnemyLayers(LayerMask layers)
    {
        enemyLayers = layers;
    }

    public void SetRatio(double r)
    {
        ratio = r;
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Debug.Log("we hit " + enemy.name);
            if (skillName == SKILL.sword)
            {
                enemy.GetComponent<PlayerController>().TakenDamage((int)(50 * ratio));
            }
            else if (skillName == SKILL.Thunder)
            {
                enemy.GetComponent<PlayerController>().TakenDamage((int)(40 * ratio), false);
            }


        }
    }
}

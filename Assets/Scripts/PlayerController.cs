using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{

    /*[SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;*/

    public GameManager gameManager;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;

    private bool m_grounded = false;
    private bool m_isDead = false;
    private bool isTutorial = false;
    // private bool m_combatIdle = false;

    public Transform attackPoint;
    public Transform buffPoint;
    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    public int attackDamage = 30;
    public int maxHealth = 150;
    public int healingAmount = 30;
    private int physicalDefense = 0;
    private float buffDuration = 10;

    // Health Text
    public TextMeshProUGUI healthText;
    // Health Bar
    public FillStatusBar statusBar;
    // Damage point Text
    public GameObject popUpText;

    int curHealth;

    public GameObject sword;
    public GameObject grandCross;
    public GameObject thunder;
    public GameObject healing;
    private Quaternion quaternion;

    /*the range between attackPoint and the skill location*/
    private Vector3 Range;
    private Vector3 shortRange;

    enum BuffType
    {
        DefenseIncrease,
        Healing
    }



    // Use this for initialization
    void Start()
    {
        
        curHealth = maxHealth;
        statusBar.SetMaxVal(maxHealth);
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        if (transform.localScale.x > 0)
        {
            quaternion = Quaternion.Euler(0, 180, 0);
            Range = new Vector3(-3, 0, 0);
            shortRange = new Vector3(-1, 0, 0);
        }
        else
        {
            quaternion = Quaternion.Euler(0, 0, 0);
            Range = new Vector3(3, 0, 0);
            shortRange = new Vector3(1, 0, 0);
        }
    }

    void Instantiate(GameObject original, Vector3 position, Quaternion rotation, LayerMask enemyLayers)
    {
        GameObject gameObject = Instantiate(original, position, rotation);
        gameObject.GetComponent<AttackSkill>().SetEnemyLayers(enemyLayers);

    }
    public void UseGrandCross(double ratio=1)
    {
        if (m_isDead) return;
        physicalDefense += 100;
        Instantiate(grandCross, buffPoint.position, quaternion);
        ShowTextPopUp("Defense++\n\n\n\n");
        StartCoroutine(RemoveBuff(buffDuration, BuffType.DefenseIncrease));
    }

    public void UseHealing(double ratio=1)
    {
        if (m_isDead) return;
        curHealth = (curHealth + healingAmount > maxHealth) ? maxHealth : curHealth + healingAmount;
        ShowDamageNumberPopUp(healingAmount, false);
        Instantiate(healing, buffPoint.position, quaternion);
    }
    public void UseSword(double ratio=1)
    {
        if (m_isDead) return;
        Instantiate(sword, attackPoint.position + shortRange, quaternion, enemyLayers);
    }
    public void UseThunder(double ratio=1)
    {
        if (m_isDead) return;
        Instantiate(thunder, attackPoint.position + Range, quaternion, enemyLayers);
    }

    public void Attack()
    {
        if (m_isDead) return;
        m_animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Debug.Log("we hit" + enemy.name);
            enemy.GetComponent<PlayerController>().TakenDamage(attackDamage);

        }

    }

    private IEnumerator RemoveBuff(float time, BuffType buff)
    {
        yield return new WaitForSeconds(time);
        if (buff == BuffType.DefenseIncrease)
        {
            physicalDefense -= 100;
        }
    }
    public void Defend()
    {
        if (m_isDead) return;
        physicalDefense = 30;
        m_animator.SetTrigger("Defend");
        StartCoroutine(RemoveBuff(buffDuration, BuffType.DefenseIncrease));
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void ShowDamageNumberPopUp(int num, bool isDamage = true)
    {
        string text = isDamage == true ? "-" : "+";
        GameObject textPopUp = Instantiate(popUpText, transform.position, Quaternion.identity);
        if (isDamage)
        {
            textPopUp.GetComponent<TextMeshPro>().color = Color.red;
        }
        else
        {
            textPopUp.GetComponent<TextMeshPro>().color = Color.green;
        }
        textPopUp.GetComponent<TextMeshPro>().text = text + num.ToString();
    }

    void ShowTextPopUp(string text, Color? color = null)
    {
        GameObject textPopUp = Instantiate(popUpText, transform.position, Quaternion.identity);
        textPopUp.GetComponent<TextMeshPro>().text = text;

    }

    // physicalDefense works on damageType True
    public void TakenDamage(int damage, bool damageType = true)
    {
        if (damageType)
        {
            if (damage > physicalDefense)
            {
                curHealth -= (damage - physicalDefense);
                ShowDamageNumberPopUp(damage - physicalDefense);
                m_animator.SetTrigger("Hurt");
                if (!GameManager.CheckAI() || gameObject.name == "LightBandit"){
                    AnalyticManager.OnSuccessDefense(false);
                }
                
            }
            else
            {
                curHealth -= 1;
                ShowDamageNumberPopUp(1);
                m_animator.SetTrigger("Defend");
                if (!GameManager.CheckAI() || gameObject.name == "LightBandit"){
                    AnalyticManager.OnSuccessDefense(true);
                }
            }
        }
        else
        {
            curHealth -= damage;
            ShowDamageNumberPopUp(damage);
            m_animator.SetTrigger("Hurt");
        }
        if (curHealth <= 0)
        {
            statusBar.UpdateStatusBar(curHealth);
            Die();
        }
    }

    void Die()
    {
        if (isTutorial)
        {
            m_animator.SetTrigger("Death");
            curHealth = maxHealth;
            new WaitForSeconds(1);
            m_animator.SetTrigger("Recover");
            return;
        }
        m_animator.SetTrigger("Death");
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        GetComponent<Collider2D>().enabled = false;
        m_isDead = true;
        this.enabled = false;
        healthText.text = "HP: 0";
        if (gameObject.name == "LightBandit")
        {
            gameManager.EndGame("Player 2");
        }
        else
        {
            gameManager.EndGame("Player 1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }


        healthText.text = "HP: " + curHealth.ToString();


        if (physicalDefense > 0){
            healthText.text += "\n Defense++";
        }
        statusBar.UpdateStatusBar(curHealth);
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }*/

        /*// -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        //Jump
        if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);*/
    }

    public int GetCurHealth() {
        return curHealth;
    }

    public void SetTutorial()
    {
        isTutorial = true;
    }
}

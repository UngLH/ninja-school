using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator amin;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private Rigidbody2D rb;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackDamage = 20f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        amin = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_timeSinceAttack += Time.deltaTime;
        if(Input.GetKeyDown("j"))
        {
            Attack();
        } else if(Input.GetKeyDown("k"))
        {
            amin.SetTrigger("Block");
        }
    }

    void Attack()
    {
        m_currentAttack++;

        if(m_currentAttack > 3)
        {
            m_currentAttack = 1;
        }

        if (m_timeSinceAttack > 1.0f)
        {
            m_currentAttack = 1;
        }

        amin.SetTrigger("Attack" + m_currentAttack);

        m_timeSinceAttack = 0.0f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
    }

    void OnDrawGizmosSelected() {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);    
    }
}

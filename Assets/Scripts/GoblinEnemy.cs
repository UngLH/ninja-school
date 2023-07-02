using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinEnemy : MonoBehaviour

{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    public int maxHealth = 100;
    public int currentHealth;

    //References
    private Animator anim;
    private GoblinPatrol enemyPatrol;

    private RaycastHit2D hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<GoblinPatrol>();
        currentHealth = maxHealth;
    }

    private void Update()
    {

        //Attack only when player in sight?
        if (PlayerInSight() && PlayerStatus.currentHealth > 0 && currentHealth > 0)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack1");
            if(hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerStatus>().TakeDamage(damage);
            }
                
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

//     private void DamagePlayer()
//     {
//         if (PlayerInSight())
//             player.TakeDamage(damage);
//     }
    public void TakeDamage(int damagePlayer) {
        cooldownTimer = 0;
        int rd = UnityEngine.Random.Range(1, PlayerStatus.crist);
        Debug.Log(rd);
        if(rd <= PlayerStatus.crist)
        {
            currentHealth -= damagePlayer*2;
        } else
        {
            currentHealth -= damagePlayer;
        }
        anim.SetTrigger("TakeHit");

        if(currentHealth <=0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        hit.collider.GetComponent<PlayerAttack>().PlayerKillEnemy();
    }
}

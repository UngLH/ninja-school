using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEnemy : MonoBehaviour
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
      private MonsterPatrol enemyPatrol;
  
      private void Awake()
      {
          anim = GetComponent<Animator>();
          enemyPatrol = GetComponentInParent<MonsterPatrol>();
          currentHealth = maxHealth;
      }
      
      public enum AttackType
      {
          Attack1,
          Attack2
      }
      private AttackType currentAttack = AttackType.Attack1;
      private void Update()
      {
          cooldownTimer += Time.deltaTime;
  
          //Attack only when player in sight?
          if (PlayerInSight())
          {
              if (cooldownTimer >= attackCooldown)
              {
                  cooldownTimer = 0;

                  // Xử lý animation tấn công dựa trên currentAttack
                  switch (currentAttack)
                  {
                      case AttackType.Attack1:
                          anim.SetTrigger("Attack1");
                          break;
                      case AttackType.Attack2:
                          anim.SetTrigger("Attack2");
                          break;
                  }

                  // Thay đổi currentAttack để chuẩn bị cho lần tấn công tiếp theo
                  if (currentAttack == AttackType.Attack1)
                  {
                      currentAttack = AttackType.Attack2;
                  }
                  else
                  {
                      currentAttack = AttackType.Attack1;
                  }
              }
          }
  
          if (enemyPatrol != null)
              enemyPatrol.enabled = !PlayerInSight();
      }
  
      private bool PlayerInSight()
      {
          RaycastHit2D hit = 
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
          currentHealth -= damagePlayer;
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
      }
}

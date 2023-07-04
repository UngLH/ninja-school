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
      public int maxHealth = 600;
      public int currentHealth;
  
      //References
      private Animator anim;
      private MonsterPatrol enemyPatrol;
      private RaycastHit2D hit;
      public GameObject winGame;
  
      private void Awake()
      {
          winGame.gameObject.SetActive(false);
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
          
  
          //Attack only when player in sight?
          if (PlayerInSight())
          {
              cooldownTimer += Time.deltaTime;
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
                  if(hit.collider.CompareTag("Player"))
                  {
                      hit.collider.GetComponent<PlayerStatus>().TakeDamage(damage);
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
          int rd = UnityEngine.Random.Range(1, 100);
          Debug.Log(rd);
       
          if(rd <= PlayerStatus.crist)
          {
              currentHealth -= damagePlayer*2;
              DamageTextManage.Myinstance.CreateText(transform.position, (damagePlayer * 2).ToString(), ColorType.SpecialDamage);
          } else
          {
              currentHealth -= damagePlayer;
              DamageTextManage.Myinstance.CreateText(transform.position, damagePlayer.ToString(), ColorType.Damage);
          }
       
          anim.SetTrigger("TakeHit");

          if(currentHealth <=0)
          {
              Die();
              DamageTextManage.Myinstance.CreateText(transform.position, "2 Coins", ColorType.CoinPlus);
          }
      }
  
      void Die()
      {
          anim.SetBool("IsDead", true);
          GetComponent<Collider2D>().enabled = false;
          this.enabled = false;
          hit.collider.GetComponent<PlayerAttack>().PlayerKillEnemy();
          winGame.gameObject.SetActive(true);
      }
}

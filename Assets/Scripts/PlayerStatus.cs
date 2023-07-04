using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static int maxHealth = 100;
    public static int damage = 20;
    public static int crist = 0;
    public static int coin = 20;
    public static int lv = 1;
    public static int currentHealth;
    public Heartbar hearthBar;
    public GameObject modalWin;
    public GameObject modalDead;
    private Animator amin;
    [SerializeField] private float timeDelay = 0.5f;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource deadSound;
    [SerializeField] private AudioSource blockSound;
    // Start is called before the first frame update
    void Start()
    {
        hearthBar.setMaxHealth(maxHealth);
        currentHealth = maxHealth;
        amin = GetComponent<Animator>();
        modalWin.gameObject.SetActive(false);
        modalDead.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hearthBar.setHealth(currentHealth);
    }

    public void setMaxHealth()
    {
        maxHealth += 20;
        Debug.Log(maxHealth);
    }

    public void TakeDamage(int dame){
        if(amin.GetCurrentAnimatorStateInfo(0).IsName("Player_blocking"))
        {
            blockSound.Play();
            return;
        }
        if(amin.GetCurrentAnimatorStateInfo(0).IsName("Player_rolling"))
        {
            return;
        }
        currentHealth -= dame;
        hurtSound.Play();
        amin.SetTrigger("TakeHit");
        if(currentHealth <=0)
        {
            hearthBar.setHealth(currentHealth);
            Die();
        }
    }

    void Die()
    {
        deadSound.Play();
        amin.SetBool("IsDead", true);
        modalDead.gameObject.SetActive(true);
        this.enabled = false;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Collider2D>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Finish"))
        {
            modalWin.gameObject.SetActive(true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
}

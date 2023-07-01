using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static int maxHealth = 100;
    public static int damage = 20;
    public static int crist = 0;
    public int currentHealth;
    public Heartbar hearthBar;
    [SerializeField] private float timeDelay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        hearthBar.setMaxHealth(maxHealth);
        currentHealth = maxHealth;
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
        currentHealth -= dame;
    }
}

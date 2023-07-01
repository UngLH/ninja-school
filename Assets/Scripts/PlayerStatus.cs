using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHealth = 50;
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

    public void TakeDamage(int dame){
        DelayAction(timeDelay);
        currentHealth -= dame;
    }

    IEnumerator DelayAction(float delayTime)
{
   //Wait for the specified delay time before continuing.
   yield return new WaitForSeconds(delayTime);
 
   //Do the action after the delay time has finished.
}
}

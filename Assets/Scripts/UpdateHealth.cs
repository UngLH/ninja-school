using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] image;
    public Button addHealth;

    void Update()
    {
        int n = (PlayerStatus.maxHealth - 100) /25;
        for(int i = 0; i < n; i++)
        {
            image[i].color = new Color32(255,99,71,255);
        }
        if(PlayerStatus.maxHealth >=200)
        {
            addHealth.gameObject.SetActive(false);
        }
    }
    
    public void SetHealth()
    {
        PlayerStatus.maxHealth += 25;
    }
}

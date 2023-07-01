using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDamage : MonoBehaviour
{
    public Image[] image;
    public Button addDamage;

    void Update()
    {
        int n = (PlayerStatus.damage - 20) /10;
        for(int i = 0; i < n; i++)
        {
            image[i].color = new Color32(255,99,71,255);
        }
        if(PlayerStatus.damage >=60)
        {
            addDamage.gameObject.SetActive(false);
        }
    }
    
    public void SetDamage()
    {
        PlayerStatus.damage += 10;
    }
}

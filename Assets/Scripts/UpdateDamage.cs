using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDamage : MonoBehaviour
{
    public Image[] image;
    public Button addDamage;
    public Text textInfo;
    private int cost;

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
        cost = 5*(n+1);

        if(PlayerStatus.coin < cost)
        {
            addDamage.interactable = false;
        }
    }
    
    public void SetDamage()
    {
        PlayerStatus.coin -= cost;
        PlayerStatus.damage += 10;
        cost += 5;
        showInfo();
    }

    public void showInfo()
    {
        textInfo.text = "You need " + cost.ToString() + " coin to upgrade this";
    }

    public void resetInfo()
    {
        textInfo.text = "";
    }
}

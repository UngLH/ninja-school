using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCrist : MonoBehaviour
{
    public Image[] image;
    public Button addCrist;
    public Text textInfo;
    private int cost;

    void Update()
    {
        int n = PlayerStatus.crist/20;
        for(int i = 0; i < n; i++)
        {
            image[i].color = new Color32(255,99,71,255);
        }
        if(PlayerStatus.crist >=80)
        {
            addCrist.gameObject.SetActive(false);
        }
        cost = 5*(n+1);
        if(PlayerStatus.coin < cost)
        {
            addCrist.interactable = false;
        }
    }
    private void checkCoin()
    {
        
    }
    
    public void SetCrist()
    {
        PlayerStatus.coin -= cost;
        PlayerStatus.crist += 20;
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

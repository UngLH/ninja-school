using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] image;
    public Button addHealth;
    public Text textInfo;
    private int cost;

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
        cost = 5*(n+1);
        if(PlayerStatus.coin < cost)
        {
            addHealth.interactable = false;
        }
    }
    
    public void SetHealth()
    {
        PlayerStatus.coin -= cost;
        PlayerStatus.maxHealth += 25;
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

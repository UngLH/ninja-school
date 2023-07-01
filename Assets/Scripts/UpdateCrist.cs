using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCrist : MonoBehaviour
{
    public Image[] image;
    public Button addCrist;

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
    }
    
    public void SetCrist()
    {
        PlayerStatus.crist += 20;
    }
}

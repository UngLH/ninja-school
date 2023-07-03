



using System;using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ColorType
{
    Damage, Heal, CoinPlus, SpecialDamage
}
public class DamageTextManage : MonoBehaviour


{
    private static DamageTextManage Instance;

    public static DamageTextManage Myinstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<DamageTextManage>();
                
            }

            return Instance;
        }
        
    }

    [SerializeField] private GameObject DamageTextPrefab;

    public void CreateText(Vector2 possition, string text, ColorType type)
    {
        TextMeshProUGUI typeText = Instantiate(DamageTextPrefab, transform).GetComponent<TextMeshProUGUI>();
        typeText.transform.position = possition;
        string sign = string.Empty;
        switch (type)
        {
            case ColorType.SpecialDamage: 
                sign += "-";
                typeText.color = Color.red;
                break;
            case ColorType.Damage: 
                sign += "-";
                typeText.color = Color.magenta;
                break;
            case ColorType.Heal:
                sign += "+";
                typeText.color = Color.green;
                break;
            case ColorType.CoinPlus:
                sign += "+";
                typeText.color = Color.yellow;
                break;
            default:
                break;
        }

        typeText.text = sign + text;
    }
}

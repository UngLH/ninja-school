using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionWin : MonoBehaviour
{
    public void GoToUpgrade()
    {
        PlayerStatus.lv++;
        PlayerStatus.coin += 10;
        SceneManager.LoadScene("UpdateStrength");
    }
}

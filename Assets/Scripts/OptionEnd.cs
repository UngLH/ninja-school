using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionEnd : MonoBehaviour
{
    public  void GoToMenu()
    {
        PlayerStatus.lv = 1;
        PlayerStatus.coin = 0;
        SceneManager.LoadScene(0);
    }
}

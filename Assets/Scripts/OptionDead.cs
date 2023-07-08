using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionDead : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToMenu()
    {
        PlayerStatus.lv = 1;
        PlayerStatus.coin = 0;
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdatePlayer : MonoBehaviour
{
    [SerializeField] private Text numCoin;
    private void Update()
    {
        numCoin.text = PlayerStatus.coin.ToString();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Map" + PlayerStatus.lv);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinInfo : MonoBehaviour
{
    private int currentCoins = 0;
    public int maxCoins;
    [SerializeField] private TMP_Text coins;
    [SerializeField] private GameObject coinPickups;
    
    private void Start() 
    {
        SetMaxCoins();
        ChangeCoinText();
    }

    private void ChangeCoinText()
    {
        coins.text = currentCoins.ToString();
    }

    public void AddCoins(int value)
    {
        currentCoins = Mathf.Clamp(currentCoins + value, 0, maxCoins);
        ChangeCoinText();
    }

    private void SetMaxCoins()
    {
        maxCoins = coinPickups.transform.childCount;
    }
}

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

    //Zmiana wyœwietlanej liczby pieniêdzy
    private void ChangeCoinText()
    {
        coins.text = currentCoins.ToString();
    }

    //Dodawanie zebranych pieniêdzy
    public void AddCoins(int value)
    {
        currentCoins = Mathf.Clamp(currentCoins + value, 0, maxCoins);
        ChangeCoinText();
    }

    //Ustawienie maksymalnej liczby pieniêdzy na liczbê obiektów pieniêdzy w hierarchi
    private void SetMaxCoins()
    {
        maxCoins = coinPickups.transform.childCount;
    }
}

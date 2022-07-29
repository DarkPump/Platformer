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

    //Zmiana wy�wietlanej liczby pieni�dzy
    private void ChangeCoinText()
    {
        coins.text = currentCoins.ToString();
    }

    //Dodawanie zebranych pieni�dzy
    public void AddCoins(int value)
    {
        currentCoins = Mathf.Clamp(currentCoins + value, 0, maxCoins);
        ChangeCoinText();
    }

    //Ustawienie maksymalnej liczby pieni�dzy na liczb� obiekt�w pieni�dzy w hierarchi
    private void SetMaxCoins()
    {
        maxCoins = coinPickups.transform.childCount;
    }
}

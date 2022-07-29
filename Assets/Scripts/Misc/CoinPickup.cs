using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //Zbieranie pieni�dzy
        if(other.CompareTag("Player") && other.GetType() == typeof(BoxCollider2D))
        {
            other.GetComponent<CoinInfo>().AddCoins(coinValue);
            gameObject.SetActive(false);
        }
    }
}

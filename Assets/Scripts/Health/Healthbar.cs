using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Healthbar : MonoBehaviour
{
    private Health playerHealth;
    private int health;
    private int numberOfHearts;
    
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Awake() 
    {
        playerHealth = GetComponent<Health>();
    }

    private void Start() 
    {
        SetMaxHearts();
    }

    public void SetMaxHearts()
    {
        numberOfHearts = playerHealth.maxHealth;
        for(int i = 0; i < hearts.Length; i ++)
        {
            if(i < numberOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    public void ChangeHeartSprite()
    {
        health = playerHealth.currentHealth;
        for(int i = 0; i < hearts.Length; i ++)
        {
            if(i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}

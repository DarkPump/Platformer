using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject gameOverUI;

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private PlayerInput playerInput; 
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if(gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    private void Start()
    {
        InputManager.instance.playerControls.UI.PauseGame.performed += ctx => CheckIfGamePaused();
    }

    private void OnDestroy()
    {
        InputManager.instance.playerControls.UI.PauseGame.performed -= ctx => CheckIfGamePaused();
    }
    //Wyœwietlenie UI przegranej gry po œmierci postaci
    public void GameOver()
    {
        gameOverUI.SetActive(true);   
    }
    //Sprawdzenie czy gra jest mo¿liwa do zapauzowania
    public void CheckIfGamePaused()
    {
        if (!pauseUI.activeInHierarchy && !gameOverUI.activeInHierarchy)
        {
            PauseGame(true);
            playerInput.enabled = false;
            GameManager.instance.UpdateGameState(GameState.Paused);
        }
    }
    //Sprawdzenie czy gra jest mo¿liwa do odpauzowania
    public void ResumeGame()
    {
        if (pauseUI.activeInHierarchy)
        {
            PauseGame(false);
            playerInput.enabled = true;
            GameManager.instance.UpdateGameState(GameState.Game);
        }
    }
    //Pauzowanie gry
    public void PauseGame(bool status)
    {
        pauseUI.SetActive(status);
        Time.timeScale = System.Convert.ToInt32(!status);
    }
    //Restartowanie gry
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = System.Convert.ToInt32(true);
    } 
    //Powrót do menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        GameManager.instance.UpdateGameState(GameState.Menu);
    }
    //Wyjœcie z gry
    public void ExitGame()
    {
        Application.Quit();
    }
    //Wystartowanie gry
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        GameManager.instance.UpdateGameState(GameState.Game);
        Time.timeScale = System.Convert.ToInt32(true);
    }
}

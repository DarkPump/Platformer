using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    public int maxHealth = 2;
    public int currentHealth;
    [System.NonSerialized] public bool isDead = false;
    private bool isInvulnerable = false;
    private Healthbar playerHealthbar;

    [Header("IFrames")]
    [SerializeField] private float iFramesDuration = 0.25f;

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Behaviour[] components;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        if(gameObject.CompareTag("Player"))
            playerHealthbar = gameObject.GetComponent<Healthbar>();

    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    //Otrzymywanie obrażeń + śmierć
    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if(currentHealth > 0)
        {
            animator.SetTrigger("Damage");

            if(gameObject.CompareTag("Player"))
                playerHealthbar.ChangeHeartSprite();
            
            StartCoroutine(Invulnerability());
        }
        else
        {
            if(gameObject.CompareTag("Player"))
                playerHealthbar.ChangeHeartSprite();

            if(!isDead)
            {
                Death();
            }
        }
    }

    //Śmierć postaci + odtworzenie animacji
    private void Death()
    {
        foreach (Behaviour component in components)
            component.enabled = false;
        if(!gameObject.CompareTag("Player"))
            GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<BoxCollider2D>().enabled = false;
        if (gameObject.CompareTag("Player"))
        {
            GameManager.instance.UpdateGameState(GameState.GameOver);
            gameObject.GetComponent<Rigidbody2D>().mass = 50;
        }
        animator.SetTrigger("Death");
        isDead = true;
    }

    //Wyłączenie objektu po śmierci (przeciwnik oraz gracz)
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    //Iframe
    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 7, true);
        yield return new WaitForSeconds(iFramesDuration);
        Physics2D.IgnoreLayerCollision(8, 7, false);
        isInvulnerable = false;
    }

    //Metoda, która dodaje życie (wykorzystywana przy zbieraniu serc)
    public void AddHealth(int value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        playerHealthbar.ChangeHeartSprite();
    }
}

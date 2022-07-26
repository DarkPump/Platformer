using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 2;
    public int currentHealth;
    private bool isDead = false;
    private bool isInvulnerable = false;
    private Healthbar playerHealthbar;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private Animator animator;
    [SerializeField] private Behaviour[] components;
    // Start is called before the first frame update
    private void Awake() 
    {
        animator = GetComponent<Animator>();
        playerHealthbar = GetComponent<Healthbar>();
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Otrzymywanie obrażeń + śmierć
    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;
        Debug.Log("TakeDamage " + maxHealth);
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if(currentHealth > 0)
        {
            animator.SetTrigger("Damage");
            if(gameObject.CompareTag("Player"))
            {
                Debug.Log("zmie� serduszka");
                gameObject.GetComponent<Healthbar>().ChangeHeartSprite();
            }
            StartCoroutine(Invulnerability());
        }
        else
        {
            if(gameObject.tag == "Player")
                gameObject.GetComponent<Healthbar>().ChangeHeartSprite();
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
        animator.SetTrigger("Death");
        isDead = true;
        Destroy(gameObject, 1);
    }

    //Iframe
    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 7, true);
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreLayerCollision(8, 7, false);
        isInvulnerable = false;
    }
}

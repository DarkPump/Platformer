using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;
    private int currentHealth;

    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        animator.SetTrigger("Damage");

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 1);
    }
}

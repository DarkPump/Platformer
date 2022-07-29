using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy parameters")]
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackVerticalForce = 5f;
    [SerializeField] private float knockbackDuration = 0.25f;

    [Header("References")]
    [SerializeField] private EnemyPatrol enemyPatrol;
    private Rigidbody2D rigidBody;
    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        if(gameObject.CompareTag("Goblin"))
        {
            if(enemyPatrol.isMoving)
                animator.SetFloat("xVelocity", 1);
            else
                animator.SetFloat("xVelocity", 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Vector2 difference = (other.transform.position - transform.position).normalized;
        Vector2 force = difference * knockbackForce;
        Player player = other.gameObject.GetComponent<Player>();
        //odrzucenie gracza
        if(player)
        {
            player.GetComponent<Health>().TakeDamage(damage);
            StartCoroutine(Knockback(knockbackForce, knockbackVerticalForce, other));
        }
    }

    private IEnumerator Knockback(float knockbackForce, float knockbackVerticalForce, Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        //Przeciwnik jest po prawej stronie
        if(transform.position.x > other.transform.position.x)
        {
            player.canMove = false;
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * knockbackVerticalForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(knockbackDuration);
            player.canMove = true;
        }
        //przeciwnik jest po lewej stronie
        else
        {
            player.canMove = false;
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * knockbackVerticalForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(knockbackDuration);
            player.canMove = true;
        }
    }
}

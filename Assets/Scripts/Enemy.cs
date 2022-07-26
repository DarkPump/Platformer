using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Health playerHealth;

    [Header("Enemy parameters")]
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackVerticalForce = 5f;
    [SerializeField] private float knockbackDuration = 0.25f;


    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        Vector2 difference = (other.transform.position - transform.position).normalized;
        Vector2 force = difference * knockbackForce;
        Debug.Log(force);
        Debug.Log(difference);
        Player player = other.gameObject.GetComponent<Player>();
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

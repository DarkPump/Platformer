using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float jumpRadius;
    public bool canMove = true;
    private float direction;
    private bool isFacingLeft;

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackDelay = 0.3f;
    private bool isAttackBlocked = false;
    

    [Header("References")]
    private Rigidbody2D rigidBody;
    private Animator animator;

    

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update() 
    {
        IsGrounded();
        ChangeDirection();
    }

    //Przypisanie wartości do kierunku zależnie od tego czy naciśnie się skręt w prawo czy w lewo
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>(); 
    }

    //Jeśli postać jest na ziemii oraz przycisk skoku został naciśnięty to skacz
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower * Time.fixedDeltaTime);
            animator.SetBool("Jump", true);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            //Zablokowanie ataku
            if(isAttackBlocked)
                return;
            //Animacja ataku
            animator.SetTrigger("Attack");
            
            //Przypisanie wszystkich trafionych przeciwników
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Zadanie obrażeń wszystkim trafionym przeciwnikom
            foreach(Collider2D enemy in enemiesHit)
            {
                enemy.GetComponent<Health>().TakeDamage(damage);
            }

            //Zablokowanie ataku
            isAttackBlocked = true;
            StartCoroutine(DelayAttack());
        }
        
    }

    //Hitbox broni
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    //Sprawdzenie czy postać jest na ziemii
    private void IsGrounded()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, jumpRadius, groundLayer); 

        if(colliders.Length > 0)
            isGrounded = true;
        animator.SetBool("Jump", !isGrounded);
        //return Physics2D.OverlapCircle(groundCheck.position, jumpRadius, groundLayer); 
    }

    //Przełącznik do odwrócenia postaci
    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    //Odwrócenie postaci przy zmianie kierunku poruszania
    private void ChangeDirection()
    {
        if(isFacingLeft && direction > 0f)
            Flip();
        else if(!isFacingLeft && direction < 0f)
            Flip();
    }

    private void FixedUpdate()
    {
        //Ruch postaci
        float movementValue = direction * speed * Time.fixedDeltaTime;
        if(canMove)
            rigidBody.velocity = new Vector2(movementValue, rigidBody.velocity.y);

        //Ustawienie wartości xVelocity na wartość prędkości postaci
        animator.SetFloat("xVelocity", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("yVelocity", rigidBody.velocity.y);
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttackBlocked = false;
    }
}

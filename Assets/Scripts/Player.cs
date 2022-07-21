using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float jumpRadius;
    private PlayerControls playerControls;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private float direction;
    private bool isFacingLeft;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRate = 2f;
    private float nextAttackTime = 0f;

    

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();

        // playerControls.Player.Movement.performed += ctx => 
        // {
        //     direction = ctx.ReadValue<float>();
        // };
        // playerControls.Player.Jump.performed += ctx =>
        // {
        //     jumpDirection = ctx.ReadValue<float>();
        // };
    }
    private void Update() 
    {
        IsGrounded();
        //Odwrócenie postaci przy zmianie kierunku poruszania
        if(isFacingLeft && direction > 0f)
            Flip();
        else if(!isFacingLeft && direction < 0f)
            Flip();
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
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
            Debug.Log("Jump");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower * Time.fixedDeltaTime);
            animator.SetBool("Jump", true);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        //Animacja ataku
        if(context.performed && isGrounded)
            animator.SetTrigger("Attack");
        
        //Przypisanie wszystkich trafionych przeciwników
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in enemiesHit)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

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

    private void FixedUpdate()
    {
        //Ruch postaci
        float movementValue = direction * speed * Time.fixedDeltaTime;
        rigidBody.velocity = new Vector2(movementValue, rigidBody.velocity.y);

        //Ustawienie wartości xVelocity na wartość prędkości postaci
        animator.SetFloat("xVelocity", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("yVelocity", rigidBody.velocity.y);
    }
}

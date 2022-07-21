using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float jumpRadius;
    [SerializeField] private Rigidbody2D rigidBody;
    private PlayerControls playerControls;
    private Vector2 movement;
    private float direction;


    private bool isFacingLeft;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    

    private void Awake()
    {
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

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            Debug.Log("Skok");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, jumpRadius, groundLayer); 
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void FixedUpdate()
    {
        float movementValue = direction * speed * Time.fixedDeltaTime;
        //rigidBody.MovePosition(rigidBody.position + new Vector2(movementValue, rigidBody.velocity.y));
        rigidBody.velocity = new Vector2(movementValue, rigidBody.velocity.y);
        // rigidBody.velocity = new Vector2(movementValue, rigidBody.velocity.y);
    }
}

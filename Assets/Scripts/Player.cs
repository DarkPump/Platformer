using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Rigidbody2D rigidBody;
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private float direction;
    private float jumpDirection;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Movement.performed += ctx => 
        {
            direction = ctx.ReadValue<float>();
        };
        playerControls.Player.Jump.performed += ctx =>
        {
            jumpDirection = ctx.ReadValue<float>();
        };
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpDirection * jumpPower);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderRange;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Colliders")]
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Layers")]
    [SerializeField] private LayerMask playerLayer;

    [Header("References")]
    [SerializeField] private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        cooldownTimer += Time.deltaTime;
        if(IsPlayerInSight())
        {
            Debug.Log("Player in sight!");
            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("Attack");
            }
        }   
    }

    public bool IsPlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * colliderRange * range * transform.localScale.x, 
            new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * colliderRange * range * transform.localScale.x, new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("Sight Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float xRangeSight;
    [SerializeField] private float yRangeSight;
    [SerializeField] private float colliderRangeSight;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Chase Parameters")]
    [SerializeField] private float xRangeChase;
    [SerializeField] private float yRangeChase;
    [SerializeField] private float colliderRangeChase;

    [Header("Colliders")]
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Layers")]
    [SerializeField] private LayerMask playerLayer;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Health playerHealth;

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
    //Sprawdzanie czy gracz jest w zasiêgu
    public bool IsPlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * colliderRangeSight * xRangeSight * transform.localScale.x, 
            new Vector2(boxCollider.bounds.size.x * xRangeSight, boxCollider.bounds.size.y * yRangeSight), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    //Sprawdzanie czy gracz jest w zasiêgu gonienia
    public bool IsPlayerInChaseRange()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * colliderRangeChase * xRangeChase * transform.localScale.x, 
            new Vector2(boxCollider.bounds.size.x * xRangeChase, boxCollider.bounds.size.y * yRangeChase), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    //Wyœwietlenie zasiêgów
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * colliderRangeSight * xRangeSight * transform.localScale.x,
         new Vector2(boxCollider.bounds.size.x * xRangeSight, boxCollider.bounds.size.y * yRangeSight));

         Gizmos.color = Color.red;
         Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * colliderRangeChase * xRangeChase * transform.localScale.x,
         new Vector2(boxCollider.bounds.size.x * xRangeChase, boxCollider.bounds.size.y * yRangeChase));
    }
}

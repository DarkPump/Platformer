using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float chaseSpeedMultiplier;
    [SerializeField] private List<Vector2> waypoints = new List<Vector2>();
    private int currentWaypointIndex = 0;
    private int nextWaypointIndex = 1;
    private bool isFacingLeft;
    [System.NonSerialized] public bool isMoving;

    [Header("References")]
    [SerializeField] private Health health;
    [SerializeField] private EnemyMelee enemyMelee;
    [SerializeField] private GameObject player;

    private void Start() 
    {
        foreach(Transform waypoint in transform)
        {
            waypoints.Add(waypoint.position);
        }
    }
    private void Update() 
    {
        if(enemyMelee != null)
        {
            if(enemyMelee.IsPlayerInChaseRange())
            {
                ChasePlayer(player);
                ChangeDirection();
            }
            else
                StartCoroutine(Patrol(waypoints));
        }
        else
            StartCoroutine(Patrol(waypoints));
    }

    //œciganie gracza
    private void ChasePlayer(GameObject player)
    {
        transform.parent.position = Vector2.MoveTowards(transform.parent.position, player.transform.position, movementSpeed * Time.deltaTime * chaseSpeedMultiplier);
    }
    //patrolowanie
    private IEnumerator Patrol(List<Vector2> waypoints)
    {
        if(!health.isDead && waypoints[currentWaypointIndex] != waypoints[nextWaypointIndex])
        {
            if(Vector2.Distance(transform.parent.position, waypoints[currentWaypointIndex]) < 0.01f)
            {
                isMoving = false;
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                nextWaypointIndex = (nextWaypointIndex + 1) % waypoints.Count;
            }
            else
            {
                isMoving = true;
                ChangeDirection();
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, waypoints[currentWaypointIndex], movementSpeed * Time.deltaTime);
                yield return new WaitForSeconds(1);
            }
        }
    }
    //odwrócenie
    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 localScale = transform.parent.localScale;
        localScale.x *= -1f;
        transform.parent.localScale = localScale;
    }

    //odwrócenie przeciwnika w kierunku gracza lub w stronê nastêpnego waypointa
    private void ChangeDirection()
    {
        if(enemyMelee != null)
        {
            if(!enemyMelee.IsPlayerInChaseRange())
        {
            if(isFacingLeft && transform.parent.position.x < waypoints[currentWaypointIndex].x)
                Flip();
            else if(!isFacingLeft && transform.parent.position.x > waypoints[currentWaypointIndex].x)
                Flip();
        }
            else
            {
                if(isFacingLeft && transform.parent.position.x < player.transform.position.x)
                    Flip();
                else if(!isFacingLeft && transform.parent.position.x > player.transform.position.x)
                    Flip();
            }
        }
        else
        {
            if(isFacingLeft && transform.parent.position.x < player.transform.position.x)
                Flip();
            else if(!isFacingLeft && transform.parent.position.x > player.transform.position.x)
                Flip();
        }
        
    }
}

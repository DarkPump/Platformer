using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private List<Vector2> waypoints = new List<Vector2>();
    private int currentWaypointIndex = 0;

    [Header("References")]
    [SerializeField] private Health health;

    private void Start() 
    {
        foreach(Transform waypoint in transform)
        {
            waypoints.Add(waypoint.position);
        }
    }
    private void Update() 
    {
        StartCoroutine(Patrol(waypoints));
        //transform.position = Vector2.MoveTowards(transform.position, moveSpots[i], speed * Time.deltaTime)
    }

    // private void Patrol(List<Transform> waypoints)
    // {
    //     //if(Vector2.Distance(transform.position, waypoints))

    //     //transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

    //     for(int i = 0; i < waypoints.Count; i++)
    //     {
    //         transform.parent.position = Vector2.MoveTowards(transform.position, waypoints[i].position, movementSpeed *Time.deltaTime);
    //     }
    // }

    private IEnumerator Patrol(List<Vector2> waypoints)
    {
        if(!health.isDead)
        {
            if(Vector2.Distance(transform.parent.position, waypoints[currentWaypointIndex]) < 0.01f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            }
            else
            {
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, waypoints[currentWaypointIndex], movementSpeed);
            }
            yield return new WaitForSeconds(1);
        }
    }
}

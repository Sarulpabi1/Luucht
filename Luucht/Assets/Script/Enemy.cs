using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public Transform[] waypoints;
    public float movementSpeed = 2f; // Vitesse de déplacement des ennemis
    private bool reachedWaypoint = false;
    private bool isMoving = true;

    void Start()
    {
        StartCoroutine(MoveToWaypoint());
    }

    IEnumerator MoveToWaypoint()
    {
        int index = Random.Range(0, waypoints.Length);
        Transform targetWaypoint = waypoints[index];

        while (targetWaypoint.GetComponent<Waypoint>().occupied)
        {
            index = Random.Range(0, waypoints.Length);
            targetWaypoint = waypoints[index];
        }

        targetWaypoint.GetComponent<Waypoint>().occupied = true;

        while (!reachedWaypoint)
        {
            if (isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, Time.deltaTime * movementSpeed);

                if (transform.position == targetWaypoint.position)
                {
                    reachedWaypoint = true;
                    isMoving = false;
                }
            }
            yield return null;
        }

        yield return null;
    }
}
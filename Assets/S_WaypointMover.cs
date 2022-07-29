using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WaypointMover : MonoBehaviour
{
    [SerializeField]
    private S_Waypoints waypoints;

    [SerializeField]
    private float moveSpeed = 5f;

    private Transform currentWaypoint;
    [SerializeField]
    private float distanceThreshold = 0.1f;

    bool arrived = false;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
            {
                if (waypoints.GetNextWaypoint(currentWaypoint) != null)
                    currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
                else
                {
                    print("Arrived!");
                    arrived = true;
                }
                   
            }
        }


    }
}

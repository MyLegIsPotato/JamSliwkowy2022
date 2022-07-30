using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WaypointMover : MonoBehaviour
{
    [SerializeField]
    public S_Waypoints waypoints;

    [SerializeField]
    private float moveSpeed = 5f;

    private Transform currentWaypoint;
    [SerializeField]
    private float distanceThreshold = 0.1f;

    bool arrived = false;
    public bool reverseTravel = false;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
    }


    Vector3 direction;
    Quaternion rotGoal;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    public float turnSpeed = .1f;


    // Update is called once per frame
    void Update()
    {
        if (!arrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
            
            direction = (currentWaypoint.position - transform.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

            if (!reverseTravel)
            {
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
            else
            {
                if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
                {
                    if (waypoints.GetPreviousWaypoint(currentWaypoint) != null)
                        currentWaypoint = waypoints.GetPreviousWaypoint(currentWaypoint);
                    else
                    {
                        print("Arrived!");
                        arrived = true;
                    }

                }
            }
        }
    }
}

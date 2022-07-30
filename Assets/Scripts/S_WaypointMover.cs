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
    private Transform prevWaypoint;
    [SerializeField]
    private float distanceThreshold = 0.1f;

    bool arrived = false;
    private bool reverseTravel = false;

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

    public void TurnBack()
    {
        reverseTravel = true;
        if(prevWaypoint != null)
            currentWaypoint = prevWaypoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaypoint != null)
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
                        prevWaypoint = currentWaypoint;
                        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
                    {
                         currentWaypoint = waypoints.GetPreviousWaypoint(currentWaypoint);
                    }
                }
            }
        }
        else
        {
            //print("Arrived!");
            arrived = true;
        }


    }
}

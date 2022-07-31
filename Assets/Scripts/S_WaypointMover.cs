using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class S_WaypointMover : MonoBehaviour
{
    [SerializeField]
    public S_Waypoints waypoints;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float distanceThreshold = 0.1f;

    [SerializeField]
    public bool useRotation = true;
    [SerializeField]
    public bool autoProceed = true;

    private Transform currentWaypoint;
    private Transform prevWaypoint;

    private Transform destination;


    //bool arrivedToTheEnd = false;
    private bool reverseTravel = false;

    public delegate void ArriveHandler();
    public ArriveHandler onArrive;
    public ArriveHandler onFinish;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize handlers
        onArrive += () => { };
        onFinish += () => { };

        currentWaypoint = waypoints.GetFirstWaypoint();
        transform.position = currentWaypoint.position;


        destination = waypoints.GetNextWaypoint(currentWaypoint);
        StartCoroutine(Move());
    }


    Vector3 direction;
    Quaternion rotGoal;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    public float turnSpeed = .1f;

    public void TurnBack()
    {
        if (!reverseTravel)
        {
            reverseTravel = true;
            ProceedToPrevious();
        }
        //StopAllCoroutines();
        //StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        if(destination != null) //If there is destination (there is none if at the end).
        {
            while (Vector3.Distance(transform.position, destination.position) > distanceThreshold) //not arrived then move towards
            {
                //print("Moving! aaa");
                if (useRotation) //should also rotate?
                {
                    direction = (destination.position - transform.position).normalized;
                    rotGoal = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
                }
                transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            //Arrived to the waypoint:
            onArrive();


            //What to do next?
            if (autoProceed)
            {
                if (reverseTravel)
                {
                    //print("Proceeding to the prev one!");
                    ProceedToPrevious();
                }
                else
                {
                    //sprint("Proceeding to the next one!");
                    ProceedToNext();

                }
                StartCoroutine(Move());
            }

            yield return null;
        }
        else
        {
            //Arrived to the end. Finish moving.
            //print("Arrived to the end!");
            onFinish();
            yield return null;
        }

    }

    public void ProceedToNext()
    {
        destination = waypoints.GetNextWaypoint(destination);
    }

    public void ProceedToPrevious()
    {
        destination = waypoints.GetPreviousWaypoint(destination);
    }
}

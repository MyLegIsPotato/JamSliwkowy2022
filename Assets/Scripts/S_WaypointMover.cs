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
    public bool autoStart = true;
    [SerializeField]
    public bool useRotation = true;
    [SerializeField]
    public bool autoProceed = true;

    private Transform prevWaypoint;


    [SerializeField]
    private Transform firstWaypoint;

    private Transform destination;


    //bool arrivedToTheEnd = false;
    private bool reverseTravel = false;

    public delegate void ArriveHandler(Transform arrived);
    public ArriveHandler onArrive;
    public ArriveHandler onDepart;
    public ArriveHandler onFinish;

    public bool altOnJunction = false;
    public bool loop = false;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize handlers
        onArrive += (x) => { };
        onDepart += (x) => { };
        onFinish += (x) => { };

        destination = firstWaypoint;
        if (firstWaypoint == null)
            firstWaypoint = waypoints.GetFirstWaypoint();

        if (autoStart)
        {
            destination = waypoints.GetNextWaypoint(firstWaypoint);
            transform.position = firstWaypoint.position;
            StartCoroutine(Move());
        }
          
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

    public void Depart()
    {
        StartCoroutine(Move());
    }

    public bool keepMoving = true;

    IEnumerator Move()
    {
        if(destination != null) //If there is destination (there is none if at the end).
        {

            //onDepart(destination);
            while (Vector3.Distance(transform.position, destination.position) > distanceThreshold) //not arrived then move towards
            {
                if (keepMoving == false)
                {
                    yield break;
                }

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
            onArrive(destination);


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
                
            }

            yield return null;
        }
        else
        {
            //Arrived to the end. Finish moving.
            //print("Arrived to the end!");
            if (loop)
                ProceedToIndex(0);
            else
            onFinish(destination);
            yield return null;
        }

    }

    public void ProceedToIndex(int index)
    {
        destination = waypoints.GetWaypointAt(0);
        StartCoroutine(Move());
    }

    public void ProceedToNext()
    {
        destination = waypoints.GetNextWaypoint(destination, altOnJunction);
        StartCoroutine(Move());
    }

    public void ProceedToPrevious()
    {
        destination = waypoints.GetPreviousWaypoint(destination, altOnJunction);
        StartCoroutine(Move());
    }
}

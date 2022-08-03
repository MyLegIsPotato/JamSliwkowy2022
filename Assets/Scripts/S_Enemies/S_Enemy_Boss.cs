using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy_Boss : S_Enemy
{
    public Transform deskWaypoint;
    public Transform safeWaypoint;
    public Transform elevatorWaypoint;
    public Transform shelfWaypoint1;
    public Transform shelfWaypoint2;
    public Transform rageWaypoint;

    public void Start()
    {
        GetComponent<S_WaypointMover>().onArrive += CheckWP;
    }

    public override void Attack()
    {
        base.Attack();

        GetComponent<Animator>().SetTrigger("Throw");
        //bullet Instantiate()
    }

    public void CheckWP(Transform waypoint)
    {
        switch (waypoint)
        {
            case var expression when waypoint == deskWaypoint:
                Debug.Log("Throw Statue");
                break;
            case var expression when waypoint == safeWaypoint:
                Debug.Log("Throw Money");

                break;
            case var expression when waypoint == elevatorWaypoint:
                Debug.Log("Kick!");

                break;
            case var expression when waypoint == shelfWaypoint1:
                Debug.Log("Throw something!");
                break;
            case var expression when waypoint == shelfWaypoint2:
                Debug.Log("Throw something else!");

                break;
            case var expression when waypoint == rageWaypoint:
                Debug.Log("RAAGEE");

                break;
            default:
                print("Normal waypoint");
                break;
        }
    }
}

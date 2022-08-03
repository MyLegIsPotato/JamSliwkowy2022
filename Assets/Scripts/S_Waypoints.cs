using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Waypoints : MonoBehaviour
{
    [Range(0, 2f)]
    [SerializeField]
    private float waypointSize = 0.4f;



    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waypointSize);
            if (t.tag == "WP_Junction")
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(t.position, waypointSize);
                foreach (Transform t2 in t)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere(t2.position, waypointSize);
                }

            }

        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Transform child = transform.GetChild(i);
            Transform childNext = transform.GetChild(i + 1);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(child.position, childNext.position);


            if (child.tag == "WP_Junction")
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(child.position, child.GetChild(0).position);

                //Gizmos.color = Color.yellow;
                for (int j = 0; j < child.childCount - 1; j++)
                {
                    Gizmos.DrawLine(child.GetChild(j).position, child.GetChild(j + 1).position);
                }
            }
        }

        //Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform GetFirstWaypoint()
    {
        return transform.GetChild(0).transform;
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        //if(currentWaypoint == null)
        //{
        //    return transform.GetChild(0);
        //}

        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        else
        {
            return null;
        }
    }

    public Transform GetNextWaypoint(Transform currentWaypoint, bool useJunction)
    {
        //if (currentWaypoint == null)
        //{
        //    return transform.GetChild(0);
        //}

        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            //If on Junction
            if (useJunction && currentWaypoint.tag == "WP_Junction")
            {
                return currentWaypoint.GetChild(0);
            }
            //If prev was Junction
            if (currentWaypoint.parent.tag == "WP_Junction")
            {
                //If prev was Junction child AND is not last child
                if (currentWaypoint.GetSiblingIndex() < currentWaypoint.parent.childCount - 1)
                    return currentWaypoint.parent.GetChild(currentWaypoint.GetSiblingIndex() + 1);
                else
                    return null;
            }
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);

        }
        else
        {
            return null;
        }
    }

    public Transform GetWaypointAt(int index)
    {
        return transform.GetChild(index).transform;
    }


    public Transform GetPreviousWaypoint(Transform currentWaypoint)
    {
        //if (currentWaypoint == null)
        //{
        //    return transform.GetChild(0);
        //}

        if (currentWaypoint.GetSiblingIndex() > 0)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() - 1);
        }
        else
        {
            return null;
        }
    }

    public Transform GetPreviousWaypoint(Transform waypointArrived, bool useJunction)
    {
        Transform waypointNext = null;

        if(waypointArrived.parent.tag != "WP_Junction")
        {
            if (waypointArrived.tag == "WP_Junction")
            {
                //print("At junction!");

                if (useJunction)
                {
                    //print("I want to go alt way!");
                    return waypointNext = waypointArrived.GetChild(0);
                }
                else
                {
                    //print("I want to go normal way.");
                    if (waypointArrived.GetSiblingIndex() > 0)
                        return transform.GetChild(waypointArrived.GetSiblingIndex() - 1);
                    else
                        return null;
                }
            }
            else
            {
                //print("On normal way back");
                if (waypointArrived.GetSiblingIndex() > 0)
                    return transform.GetChild(waypointArrived.GetSiblingIndex() - 1);
                else
                    return null;
            }
        }
        else
        {
            //print("I want to go alt way!");
            if(waypointArrived.GetSiblingIndex() < waypointArrived.parent.childCount - 1)
            {
                return waypointArrived.parent.GetChild(waypointArrived.GetSiblingIndex() + 1);

            }
            else
            {
                return null;
            }
        }
    }
}

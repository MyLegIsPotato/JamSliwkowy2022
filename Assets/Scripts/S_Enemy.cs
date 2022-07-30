using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy : MonoBehaviour
{

    private int health;
    public int Health
    {
        get { return health; } 
        set { health = value; }
    }

    public int damage; 

    private void OnMouseDown()
    {
        this.GetComponent<S_WaypointMover>().TurnBack();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000))
        {
            print("Ouch! That Ray hit me, " + this.name);
        }

        //Call a static event that "SOME" enemy was hit.
        S_EnemyManager.OnEnemyHit(hit.point);
    }
}

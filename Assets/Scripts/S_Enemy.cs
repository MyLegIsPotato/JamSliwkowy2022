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
        print("Ouch! It's me, " + this.name);


        this.GetComponent<S_WaypointMover>().reverseTravel = true;
    }
}

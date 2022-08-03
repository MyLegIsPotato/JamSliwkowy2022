using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon_Fuck : S_Weapon
{
    public override void TryWeaponShoot()
    {
        //nadpisuje cos z S_Weapon
        base.TryWeaponShoot();
        
    }

    public override void EnemyReaction(Vector3 hitLocation, S_Enemy _e)
    {
        base.EnemyReaction(hitLocation, _e);
        _e.GetComponent<S_WaypointMover>().altOnJunction = true;
        //print("... I'm going to the boss.");
    }
}

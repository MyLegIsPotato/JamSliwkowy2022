using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon_Welcome : S_Weapon
{
    public override void EnemyReaction(Vector3 hitLocation, S_Enemy _e)
    {
        base.EnemyReaction(hitLocation, _e);
        print("... I like you!.");
    }
}

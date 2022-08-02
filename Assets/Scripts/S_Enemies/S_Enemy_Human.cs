using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy_Human : S_Enemy
{
    public bool isShy = true;
    [SerializeField]
    private AudioClip notShyMp3;

    //Emotional Help
    public GameObject emoPositive_hitFX_Prefab;
    public List<AudioClip> emoPositive_hitFX_Prefab_hitSFXs;

    public override void EnemyReaction()
    {
        print("I'm (human) hit.");

        if (Random.Range(0, 10) == 0)
            isShy = false;
        if (isShy)
            GetComponent<S_WaypointMover>().TurnBack();
        else
        {
            //if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().PlayOneShot(notShyMp3);
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    public override void Hit(Vector3 hitLocation, S_Weapon _wp)
    {
        base.Hit(hitLocation, _wp);
        if(_wp.emotionalDamage < 0)
        {
            Happiness += _wp.emotionalDamage;

            //Spawn sprite FX
            GameObject go = Instantiate(emoPositive_hitFX_Prefab);
            go.transform.position = hitLocation;

            //Play sound FX
            GetComponent<AudioSource>().PlayOneShot(emoPositive_hitFX_Prefab_hitSFXs[Random.Range(0, emoPositive_hitFX_Prefab_hitSFXs.Count - 1)]);
        }

    }
}

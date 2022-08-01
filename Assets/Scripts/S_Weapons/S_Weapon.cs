using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    [SerializeField]
    public string weaponName = "Default";
    [SerializeField]
    public float weaponDamage = 0;
    public float weaponShootInterval = 1;
    public float weaponReloadTime = 3;
    public float weaponPOINTS = 1;


    public Sprite weaponBarGraphic;
    public AudioClip weaponShootSound;
    public AudioClip weaponReloadSound;
    public List<AudioClip> weaponHitSounds;
    public GameObject hitFX_Prefab;
    public GameObject missFX_Prefab;



    public S_Weapon()
    {
        weaponName = "placeholder";
    }

    public virtual void WeaponShoot()
    {

    }
    
    public void Select()
    {
        //Debug.LogError("Adding events!");
        S_EnemyManager.OnEnemyHit += EnemyReaction;
        S_EnemyManager.OnEnemyHit += EnemyHitFX;
        S_EnemyManager.OnEnemyHit += EnemyHitSFX;
    }

    public void Deselect()
    {
        S_EnemyManager.OnEnemyHit -= EnemyReaction;
        S_EnemyManager.OnEnemyHit -= EnemyHitFX;
        S_EnemyManager.OnEnemyHit -= EnemyHitSFX;
    }

    public virtual void EnemyReaction(Vector3 hitLocation, S_Enemy _e)
    {
        print("I'm hit...");
        //Do something else...
    }

    public void EnemyHitFX(Vector3 hitLocation, S_Enemy _e)
    {
        GameObject go = Instantiate(hitFX_Prefab);
        go.transform.position = hitLocation;
    }
    public void EnemyHitSFX(Vector3 hitLocation, S_Enemy _e)
    {
        _e.GetComponent<AudioSource>().PlayOneShot(weaponHitSounds[Random.Range(0, weaponHitSounds.Count - 1)]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    public S_WeaponSystem weaponSystem;

    [SerializeField]
    public string weaponName = "Default";
    [SerializeField]
    public int weaponDamage = 0;
    public int emotionalDamage = 0;
    public float weaponShootInterval = 1;
    public int weaponReloadTime = 3;
    public int weaponPOINTS = 1;

    public Sprite weaponBarGraphic;
    public AudioClip weaponShootSound;
    public AudioClip weaponReloadSound;
    public GameObject missFX_Prefab;

    public Sprite bulletSprite;

    public S_Weapon()
    {
        weaponName = "placeholder";
    }

    public virtual void WeaponShoot()
    {
        if(weaponShootSound != null)
            GetComponent<AudioSource>().PlayOneShot(weaponShootSound);
    }

    public virtual void WeaponReload()
    {
        if(weaponReloadSound != null)
            GetComponent<AudioSource>().PlayOneShot(weaponReloadSound);
    }
    
    public virtual void Select()
    {
        weaponSystem.GetComponent<S_UI_Animator>().ammoIcon.GetComponent<UnityEngine.UI.Image>().sprite = bulletSprite; 

    }

    public void Deselect()
    {
        //S_EnemyManager.OnEnemyHit -= EnemyReaction;
    }

    public virtual void EnemyReaction(Vector3 hitLocation, S_Enemy _e)
    {
        print("I'm hit..."); //TO DO move 

        //_e.EnemyHitFX(hitLocation, _e);
        //_e.EnemyHitSFX(hitLocation, _e);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    public S_WeaponSystem weaponSystem;

    //Set in inspector
    [SerializeField]
    public string weaponName = "Default";
    [SerializeField]
    public int weaponDamage = 0;
    public int emotionalDamage = 0;
    public float weaponShootInterval = 1;
    public int weaponReloadTime = 3;
    public bool limitedAmmo = false;
    public int weaponAmmoCapacity = 0;
    public int weaponPOINTS = 1;
    public float weaponRange = 100;

    public Sprite weaponBarGraphic;
    public AudioClip weaponShootSound;
    public AudioClip weaponReloadSound;
    public GameObject missFX_Prefab;

    public Sprite ammoCounterSprite;

    //Runtime
    private int weaponCurrentAmmo; 
    public float lastRofTime = 0;

    public bool isReloading = false;

    public S_Weapon()
    {
        weaponName = "placeholder";
    }

    public virtual void TryWeaponShoot()
    {
        print(isReloading);
        //Don't let player shoot when not enough time has passed since the last shot.
        if (Time.time > lastRofTime + weaponShootInterval && !isReloading)
        {
            if (limitedAmmo)
            {
                if(weaponCurrentAmmo > 0) //Some ammo left
                {
                    WeaponShoot();
                }
                else //Empty Mag
                {
                    weaponSystem.PlayDenySound();
                    weaponSystem.GetComponent<S_UI_Animator>().PromptReload();
                    weaponSystem.GetComponent<S_UI_Animator>().AnimateReloadPrompt();
                    weaponSystem.GetComponent<S_UI_Animator>().ammoIcon.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
                }
            }
            else
            {
                WeaponShoot();
            }
        }
    }

    private void WeaponShoot()
    {
        //Play sound, Animate Cursor if not loading...
        print("Shoot!");
        S_CursorManager.OnAnyWeaponShoot();
        //Sound
        if (weaponShootSound != null)
            weaponSystem.GetComponent<AudioSource>().PlayOneShot(weaponShootSound);
        //Animation
        weaponSystem.GetComponent<S_CursorManager>().AnimateCursor(weaponShootInterval);
        lastRofTime = Time.time;

        //Set ammo bar fill
        UpdateAmmoBar();

        //Raycast + Decide if damage enemy
        weaponSystem.GetComponent<S_CursorManager>().ShootRaycast(weaponSystem);

        weaponCurrentAmmo--;
    }



    public virtual void WeaponReload()
    {
        isReloading = true;
        weaponSystem.StartCoroutine(weaponSystem.ReloadProcess());

    }

    public virtual void Select()
    {
        weaponSystem.GetComponent<S_UI_Animator>().ammoIcon.GetComponent<UnityEngine.UI.Image>().sprite = ammoCounterSprite;
        UpdateAmmoBar();
    }

    public void UpdateAmmoBar()
    {
        if (limitedAmmo)
            weaponSystem.GetComponent<S_UI_Animator>().ammoIcon.GetComponent<UnityEngine.UI.Image>().fillAmount = Mathf.InverseLerp(0, weaponAmmoCapacity, weaponCurrentAmmo);
        else
            weaponSystem.GetComponent<S_UI_Animator>().ammoIcon.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;
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

    internal void MaxAmmo()
    {
        weaponCurrentAmmo = weaponAmmoCapacity;
        UpdateAmmoBar();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    [SerializeField]
    public string weaponName = "Default";
    [SerializeField]
    public float weaponDamage = 0;
    public float weaponRateOfFire = 1;
    public float weaponReloadTime = 3;
    public float weaponPOINTS = 1;


    public Sprite weaponGraphic;
    public AudioClip weaponSound;
    public GameObject hitFX_Prefab;
}

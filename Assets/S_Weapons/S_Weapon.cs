using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    [SerializeField]
    public string weaponName = "Default";
    [SerializeField]
    public float weaponDamage;
    public float weaponRateOfFire;
    public float weaponReloadTime;


    public Sprite weaponGraphic;
    public AudioClip weaponSound;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class S_PlayerManager : MonoBehaviour
{
    //HP:
    public bool canDie = false;

    [SerializeField]
    public Image HealthSlider;

    private float playerHealth = 100;

    public float maxHP = 100;

    public float PlayerHealth
    {
        get { return playerHealth; }
        set {

            if (canDie)
            {
                playerHealth = value;
            }
            else
            {
                if(playerHealth < 10)
                {
                    playerHealth = 10;
                }
                else
                {
                    playerHealth = value;
                }
            }
            HealthSlider.fillAmount = Mathf.InverseLerp(0, maxHP, playerHealth);
        }
    }

    [Range(0,1f)]
    public float hitAnimationTimer;

    //POINTS:
    [SerializeField]
    public Image PointsSlider;

    public float maxPOINTS = 100;

    private float currentPOINTS = 0;
    public float CurrentPOINTS
    {
        get { return currentPOINTS; }
        set
        {
            currentPOINTS = value;
            PointsSlider.fillAmount = Mathf.InverseLerp(0, maxPOINTS, currentPOINTS);
        }
    }

    public void AddPOINTS(Vector3 hitLocation, S_Enemy _e)
    {
        CurrentPOINTS += GetComponent<S_WeaponSystem>().GetCurrentWeapon.weaponPOINTS;
    }

    public void GetHit()
    {
        PlayerHealth -= 1;
    }


    private void Start()
    {
        S_EnemyManager.OnEnemyHit += AddPOINTS;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta == new Vector2(0, 1))
        {
            GetComponent<S_WeaponSystem>().SelectedWeaponIndex++;
        } else if (Input.mouseScrollDelta == new Vector2(0, -1))
        {
            GetComponent<S_WeaponSystem>().SelectedWeaponIndex--;
        }
    }
}

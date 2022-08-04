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

    [SerializeField]
    AudioClip hitSFX;
    [SerializeField]
    AudioClip ultraGoreSFX;

    public float PlayerHealth
    {
        get { return playerHealth; }
        set {

            if(value <= 100) 
            {
                if (canDie)
                {
                    playerHealth = value;
                    if(playerHealth < 0)
                    {
                        GetComponent<S_UI_Animator>().ShowDeathScreen();
                        GetComponent<AudioSource>().PlayOneShot(ultraGoreSFX);
                        Camera.main.GetComponent<ShaderEffect_CorruptedVram>().enabled = true;
                        Camera.main.GetComponent<ShaderEffect_CorruptedVram>().shift = 60;
                        Time.timeScale = 0.05f;
                    }
                }
                else
                {
                    if (playerHealth < 10)
                    {
                        playerHealth = 10;
                    }
                    else
                    {
                        playerHealth = value;
                    }
                }
            }
            else
            {
                playerHealth = 100;
            }

 
            HealthSlider.fillAmount = Mathf.InverseLerp(0, maxHP, playerHealth);
        }
    }

    [Range(0,1f)]
    public float hitAnimationTimer;

    [SerializeField]
    AudioClip screenCrackSFX;

    //POINTS:
    [SerializeField]
    public Image PointsSlider;
    public float maxPOINTS = 800;
    private float currentPOINTS = 0;
    public float CurrentPOINTS
    {
        get { return currentPOINTS; }
        set
        {
            if (value < 0)
            {
                currentPOINTS = 0;
            }
            else
            {
                currentPOINTS = value;
            }

            if (value >= 900)
            {
                GetComponent<S_UI_Animator>().pointsInfo.SetActive(true);
            }

            PointsSlider.fillAmount = Mathf.InverseLerp(0, maxPOINTS, currentPOINTS);
        }
    }

    public void CrackScreen()
    {
        GetComponent<S_UI_Animator>().screenCracks.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(screenCrackSFX);
        GetComponentInChildren<ShaderEffect_Unsync>().StartCoroutine(GetComponentInChildren<ShaderEffect_Unsync>().Unsync());
        PlayerHealth -= 10;
    }

    public void GlitchScreen()
    {
        GetComponentInChildren<ShaderEffect_BleedingColors>().StartCoroutine(GetComponentInChildren<ShaderEffect_BleedingColors>().GlitchScreen());
        PlayerHealth -= 5;
    }

    public void AddPOINTS(Vector3 hitLocation, S_Enemy _e, S_Weapon _wp)
    {
        CurrentPOINTS += _wp.weaponPOINTS * _e.POINTS_Multiplier;
    }

    public void GetHit(int damage)
    {
        PlayerHealth -= damage;
        GetComponent<S_UI_Animator>().GetHitHeartsAnim();
        GetComponent<AudioSource>().PlayOneShot(hitSFX);
    }

    

    private void Start()
    {
        S_EnemyManager.OnEnemyHit += AddPOINTS;
    }

    private void Update()
    {

    }
}

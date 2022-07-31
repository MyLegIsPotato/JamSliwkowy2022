using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class S_PlayerManager : MonoBehaviour
{
    private int playerHealth;
    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    //POINTS:
    [SerializeField]
    public Image PointsSlider;

    public float maxLevelPOINTS = 100;

    private float currentPOINTS = 0;
    public float CurrentPOINTS
    {
        get { return currentPOINTS; }
        set
        {
            currentPOINTS = value;
            PointsSlider.fillAmount = Mathf.InverseLerp(0, maxLevelPOINTS, currentPOINTS);
        }
    }

    public void AddPOINTS(Vector3 hitLocation, S_Enemy _e)
    {
       CurrentPOINTS += weapons[selectedWeaponIndex].weaponPOINTS;
    }

    //Weapons:
    public S_UI_Animator UI_Animator;
    private int selectedWeaponIndex;

    public int SelectedWeaponIndex { 
        get { return selectedWeaponIndex; } 
        set {
           

             selectedWeaponIndex = value;
            if (selectedWeaponIndex < 0)
            {
                //UI_Animator.BounceOnOutOfBounds(selectedWeaponIndex);

                //Fix Out of bounds and play error sound
                selectedWeaponIndex = 0;
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().PlayOneShot(weaponOutOfBounds);
            }
            else if(selectedWeaponIndex > weapons.Count - 1)
            {
                //UI_Animator.BounceOnOutOfBounds(selectedWeaponIndex);

                //Fix Out of bounds and play error sound
                selectedWeaponIndex = weapons.Count - 1;
                if(!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().PlayOneShot(weaponOutOfBounds);
            }

            UI_Animator.SwitchTo(selectedWeaponIndex);

            print(selectedWeaponIndex);
        } 
    } 

    [SerializeField]
    List<S_Weapon> weapons;

    [SerializeField]
    AudioClip weaponOutOfBounds;


    public S_Weapon GetCurrentWeapon
    {
        get { return weapons[selectedWeaponIndex]; }
    }

    public delegate void WeaponAddedHandler();
    public WeaponAddedHandler OnWeaponAdded;

    private void AddWeapon(S_Weapon newWeapon)
    {
        weapons.Add(newWeapon);
        OnWeaponAdded();
    }

    private void Start()
    {
        S_EnemyManager.OnEnemyHit += AddPOINTS;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta == new Vector2(0, 1))
        {
            SelectedWeaponIndex++;
        } else if (Input.mouseScrollDelta == new Vector2(0, -1))
        {
            SelectedWeaponIndex--;
        }
    }
}

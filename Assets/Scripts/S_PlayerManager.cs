using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class S_PlayerManager : MonoBehaviour
{
    public S_UI_Animator UI_Animator;
    private int selectedWeaponIndex;

    [SerializeField]
    List<S_Weapon> weapons;

    public delegate void WeaponAddedHandler();
    public WeaponAddedHandler OnWeaponAdded;

    private int playerHealth;
    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value;}
    }

    private void Start()
    {
        
    }

    private void AddWeapon(S_Weapon newWeapon)
    {
        weapons.Add(newWeapon);
        OnWeaponAdded();
    }

    private void Update()
    {
        if(UI_Animator.isPlaying == false){
            if (Input.mouseScrollDelta == new Vector2(0, 1))
            {
                print("weapon Try Up");
                UI_Animator.WeaponUp();
                selectedWeaponIndex++;

            }
            else if (Input.mouseScrollDelta == new Vector2(0, -1))
            {
                print("weapon Try Down");
                UI_Animator.WeaponDown();
                selectedWeaponIndex--;
            }
        }
    }
}

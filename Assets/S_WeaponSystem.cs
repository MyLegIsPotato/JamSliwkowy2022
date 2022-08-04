using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WeaponSystem : MonoBehaviour
{
    [SerializeField]
    public List<S_Weapon> allWeapons = new List<S_Weapon>();

    [SerializeField]
    public List<S_Weapon> activeWeapons = new List<S_Weapon>();

    public S_Weapon GetCurrentWeapon
    {
        get
        {
            if (activeWeapons.Count > 0)
                return activeWeapons[selectedWeaponIndex];
            else
                return null;
        }
    }

    private int selectedWeaponIndex = 0;
    public int SelectedWeaponIndex
    {
        get { return selectedWeaponIndex; }
        set
        {
            if (value < 0)
            {
                //UI_Animator.BounceOnOutOfBounds(selectedWeaponIndex);

                //Fix Out of bounds and play error sound
                selectedWeaponIndex = 0;
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().PlayOneShot(weaponOutOfBounds);
            }
            else if (value > activeWeapons.Count - 1)
            {
                //UI_Animator.BounceOnOutOfBounds(selectedWeaponIndex);

                //Fix Out of bounds and play error sound
                selectedWeaponIndex = activeWeapons.Count - 1;
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().PlayOneShot(weaponOutOfBounds);
            }
            else //in bounds
            {

                GetCurrentWeapon.Deselect();

                selectedWeaponIndex = value;

                GetComponent<S_UI_Animator>().SwitchTo(selectedWeaponIndex);


                GetCurrentWeapon.Select();
            }
        }
    }

    public void Start()
    {
        S_ElevatorController.OnElevatorArrived += (x) => { GiveWeaponOnFloor(x); };
        //GiveAllWeapons();

    }

    public void GiveAllWeapons() 
    { 
        foreach(S_Weapon wp in allWeapons)
        {
            ActivateWeapon(wp);
        }
    }

    public void GiveWeaponOnFloor(int floorNumber)
    {
        switch (floorNumber)
        {
            case 0:
                break;
            case 1:
                ActivateWeapon(0);
                ActivateWeapon(1);
                break;
            case 2:
                ActivateWeapon(2);
                break;
            case 3:
                ActivateWeapon(3);
                break;
            case 4:
                break;
            case 5:
                ActivateWeapon(4);
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                ActivateWeapon(5);
                break;
            default:
                break;
        }
    }

    [SerializeField]
    AudioClip weaponOutOfBounds;

    public delegate void WeaponAddedHandler(S_Weapon _w);
    public WeaponAddedHandler OnWeaponAdded;

    public void ActivateWeapon(int index)
    {
        S_Weapon weapon = allWeapons[index];
        weapon.weaponSystem = this;
        weapon.MaxAmmo();
        weapon.lastRofTime = 0;
        weapon.isReloading = false;

        if(!activeWeapons.Contains(weapon))
            activeWeapons.Add(weapon);

        SelectedWeaponIndex = activeWeapons.IndexOf(weapon);

        GetComponent<S_UI_Animator>().AddIcon(weapon, SelectedWeaponIndex);

    }

    public void ActivateWeapon(S_Weapon weapon)
    {
        weapon.weaponSystem = this;
        weapon.MaxAmmo();
        weapon.lastRofTime = 0;
        weapon.isReloading = false;

        if (!activeWeapons.Contains(weapon))
            activeWeapons.Add(weapon);

        SelectedWeaponIndex = activeWeapons.IndexOf(weapon);

        GetComponent<S_UI_Animator>().AddIcon(weapon, SelectedWeaponIndex);
    }

    private void Update()
    {
        if(GetCurrentWeapon != null)
        {
            if (!GetCurrentWeapon.isReloading)
            {
                if (Input.mouseScrollDelta == new Vector2(0, 1))
                {
                    if (GetComponent<S_CursorManager>().animTimer == 0)
                        SelectedWeaponIndex++;
                    else
                        PlayDenySound();

                }
                else if (Input.mouseScrollDelta == new Vector2(0, -1))
                {
                    if (GetComponent<S_CursorManager>().animTimer == 0)
                        SelectedWeaponIndex--;
                    else
                        PlayDenySound();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {

                    GetCurrentWeapon.WeaponReload();
                    GetComponent<S_UI_Animator>().reloadPrompt.SetActive(false);
                }

            }
        }


    }

    public void PlayDenySound()
    {
        GetComponent<AudioSource>().PlayOneShot(weaponOutOfBounds);

    }
    public IEnumerator ReloadProcess()
    {
        S_Weapon _wp = GetCurrentWeapon;

        if (_wp.weaponReloadSound != null)
            GetComponent<AudioSource>().PlayOneShot(_wp.weaponReloadSound);
        yield return GetComponent<S_CursorManager>().AnimateCursor360(_wp.weaponReloadTime);
        _wp.isReloading = false;
        _wp.MaxAmmo();

        yield return null;
    }

}

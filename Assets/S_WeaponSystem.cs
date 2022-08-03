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
        ActivateWeapon(0);
        ActivateWeapon(1);
        ActivateWeapon(2);
        ActivateWeapon(3);
        ActivateWeapon(4);
        ActivateWeapon(5);



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
        activeWeapons.Add(weapon);
        SelectedWeaponIndex = activeWeapons.IndexOf(weapon);

        GetComponent<S_UI_Animator>().AddIcon(weapon, SelectedWeaponIndex);

    }

    private void Update()
    {

        if (Input.mouseScrollDelta == new Vector2(0, 1))
        {
            if (GetComponent<S_CursorManager>().animTimer == 0)
                SelectedWeaponIndex++;
            else
                GetComponent<AudioSource>().PlayOneShot(weaponOutOfBounds);

        }
        else if (Input.mouseScrollDelta == new Vector2(0, -1))
        {
            if (GetComponent<S_CursorManager>().animTimer == 0)
                SelectedWeaponIndex--;
            else
                GetComponent<AudioSource>().PlayOneShot(weaponOutOfBounds);

        }
    }
}

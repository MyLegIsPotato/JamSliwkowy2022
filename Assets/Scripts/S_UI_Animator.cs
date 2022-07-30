using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UI_Animator : MonoBehaviour
{
    public LTDescr currentlyPlaying;
    public bool isPlaying = false;

    public float weaponChangeTime = 0.3f;
    public float weaponWidth;

    public GameObject weaponBar;

    public void WeaponUp()
    {
        isPlaying = true;
        currentlyPlaying = LeanTween.moveLocalX(weaponBar, weaponBar.transform.localPosition.x - weaponWidth, weaponChangeTime).setOnComplete(() => isPlaying = false);
    }

    public void WeaponDown()
    {
        isPlaying = true;
        currentlyPlaying = LeanTween.moveLocalX(weaponBar, weaponBar.transform.localPosition.x + weaponWidth, weaponChangeTime).setOnComplete(() => isPlaying = false); ;
    }
}

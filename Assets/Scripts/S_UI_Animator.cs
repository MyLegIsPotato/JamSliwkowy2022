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
    public GameObject ammoIcon;

    public void Start()
    {
        S_CursorManager.OnLMBDown += onPlayerShoot;
    }

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

    public void SwitchTo(int weaponIndex)
    {
        currentlyPlaying = LeanTween.moveLocalX(weaponBar, weaponIndex * -135, weaponChangeTime).setOnComplete(() => isPlaying = false); ;
    }

    public void onPlayerShoot()
    {
        //print("kupsko");
        LTSeq seq = LeanTween.sequence();
        seq.append(LeanTween.moveLocalY(ammoIcon, 10, 0.2f));
        seq.append(LeanTween.moveLocalY(ammoIcon, 0, 0.2f));
        seq.setScale(1f);
    }

    public void BounceOnOutOfBounds(int weaponIndex) //Not working, To do - Maybe.
    {
        //LTSeq seq = LeanTween.sequence();
        //if(weaponIndex < -1)
        //{
        //    seq.append(LeanTween.moveLocalX(weaponBar, 135, 0.2f).setEaseLinear());
        //    seq.append(0.5f);
        //    seq.append(LeanTween.moveLocalX(weaponBar, -135, 0.2f).setEaseLinear());
        //    print("what1");
        //}
        //else
        //{
        //    seq.append(LeanTween.moveLocalX(weaponBar, -135, 0.2f).setEaseLinear());
        //    seq.append(0.5f);
        //    seq.append(LeanTween.moveLocalX(weaponBar, 135, 0.2f).setEaseLinear());
        //    print("what2");
        //}

        ////seq.setScale(1f);
    }
}

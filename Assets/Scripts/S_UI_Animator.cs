using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Animator : MonoBehaviour
{
    public LTDescr currentlyPlaying;
    public bool isPlaying = false;

    public float weaponChangeTime = 0.3f;
    public float weaponWidth;

    public GameObject weaponBar;
    public GameObject weaponBarChild;
    public GameObject weaponBarIconPlaceholder;
    public GameObject ammoIcon;

    public GameObject screenCracks;
    public GameObject bossHPBar;
    public GameObject reloadPrompt;

    public void Start()
    {
        S_CursorManager.OnAnyWeaponShoot += ShootAmmoAnim;
    }

    public void AddIcon(S_Weapon _w, int barIndex)
    {
        GameObject go = Instantiate(weaponBarIconPlaceholder, weaponBarChild.transform);
        go.GetComponent<Image>().sprite = _w.weaponBarGraphic;
        go.name = _w.name;
        go.transform.localPosition = new Vector2(barIndex * 200, 0);
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
        currentlyPlaying = LeanTween.moveLocalX(weaponBar, weaponIndex * -200, weaponChangeTime).setOnComplete(() => isPlaying = false); ;
    }

    public void ShootAmmoAnim()
    {
        LTSeq seq = LeanTween.sequence();
        seq.append(LeanTween.moveLocalY(ammoIcon, 10, 0.2f));
        seq.append(LeanTween.moveLocalY(ammoIcon, 0, 0.2f));
        seq.setScale(1f);
    }

    public void PromptReload()
    {
        reloadPrompt.SetActive(true);
    }

    public void AnimateReloadPrompt()
    {
        LTSeq seq = LeanTween.sequence();
        seq.append(LeanTween.scale(reloadPrompt, Vector2.one * 1.2f, 0.5f));
        seq.append(LeanTween.scale(reloadPrompt, Vector2.one, 0.5f));

        seq.setScale(1f);
    }
}

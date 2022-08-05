using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class S_CursorManager : MonoBehaviour
{
    public delegate void LMBDownHandler();
    public static LMBDownHandler OnAnyWeaponShoot;

    public Texture2D cursorDefault;
    [SerializeField]
    private Canvas playerUI;


    [SerializeField]
    Image cursorImage;

    [SerializeField]
    AudioClip deny;

    void OnDisable()
    {
        OnAnyWeaponShoot = null;
    }

    private void Start()
    {

    }

    void Awake()
    {
        Cursor.visible = false;
        //Cursor.SetCursor(cursorDefault, new Vector2(cursorDefault.width/2, cursorDefault.height/2), CursorMode.ForceSoftware);
    }

    public void Update()
    {
        cursorImage.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if(GetComponent<S_WeaponSystem>().GetCurrentWeapon != null)
        {
            if (GetComponent<S_WeaponSystem>().GetCurrentWeapon.automaticFire == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GetComponent<S_WeaponSystem>().GetCurrentWeapon.TryWeaponShoot(); //Will work if not reloading           
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    GetComponent<S_WeaponSystem>().GetCurrentWeapon.TryWeaponShoot(); //Will work if not reloading           

                }
            }

        }



    }

    public void ShootRaycast(S_WeaponSystem weaponSystem)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weaponSystem.GetCurrentWeapon.weaponRange))
        {
            GameObject hitObject = hit.collider.gameObject;
            //Check what object was hit by the ray:
            if (hitObject.GetComponent<S_Enemy>())
            {
                //Enemy
                hitObject.GetComponent<S_Enemy>().Hit(hit.point, weaponSystem.GetCurrentWeapon);
            }
            else
            {
                LTSeq seq = LeanTween.sequence();
                seq.append(LeanTween.scale(cursorImage.gameObject, Vector2.one * 1.2f, 0.1f));
                seq.append(LeanTween.scale(cursorImage.gameObject, Vector2.one * 0.8f, 0.1f));
                seq.append(LeanTween.scale(cursorImage.gameObject, Vector2.one * 1.2f, 0.1f));
                seq.append(LeanTween.scale(cursorImage.gameObject, Vector2.one * 0.8f, 0.1f));
                GetComponent<AudioSource>().PlayOneShot(deny, 0.4f);
                seq.setScale(1f);
                //StartCoroutine(ShakeCursor());
            }
        }
        else
        {
                LTSeq seq = LeanTween.sequence();
                seq.append(LeanTween.scale(cursorImage.gameObject, Vector2.one * 1.2f, 0.1f));
                seq.append(LeanTween.scale(cursorImage.gameObject, Vector2.one * 0.8f, 0.1f));
                seq.append(LeanTween.scale(cursorImage.gameObject, Vector2.one * 1.2f, 0.1f));
                seq.append(LeanTween.scale(cursorImage.gameObject, Vector2.one * 0.8f, 0.1f));
                GetComponent<AudioSource>().PlayOneShot(deny, 0.4f);
                seq.setScale(1f);
                //StartCoroutine(ShakeCursor());
        }

        Debug.DrawLine(ray.origin, hit.point, Color.green, 3f);
    }

    public float animTimer = 0;

    public void AnimateCursor(float duration)
    {
        StartCoroutine(AnimateCursor360(duration));
    }

    public IEnumerator ShakeCursor()
    {


        yield return null;
    }

    public IEnumerator AnimateCursor360(float duration)
    {
        animTimer = 0;
        cursorImage.fillClockwise = false;
        //GetComponent<AudioSource>().PlayOneShot(GetComponent<S_WeaponSystem>().GetCurrentWeapon.weaponReloadSound);
        while (animTimer < duration/2)
        {
            cursorImage.fillAmount = Mathf.InverseLerp(duration / 2, 0, animTimer);

            animTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        animTimer = 0;
        cursorImage.fillClockwise = true;
        while (animTimer < duration/ 2)
        {
            cursorImage.fillAmount = Mathf.InverseLerp(0,duration/ 2, animTimer);

            animTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        animTimer = 0;
        yield return null;
    }
}

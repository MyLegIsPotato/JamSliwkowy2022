using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class S_CursorManager : MonoBehaviour
{
    public delegate void LMBDownHandler();
    public static LMBDownHandler OnLMBDown;

    public Texture2D cursorDefault;
    [SerializeField]
    private Canvas playerUI;

    private float lastRofTime = 0;

    [SerializeField]
    Image cursorImage;

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


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Don't let player shoot when not enough time has passed since the last shot.
            if (Time.time > lastRofTime + GetComponent<S_WeaponSystem>().GetCurrentWeapon.weaponShootInterval)
            {
                print("Shoot!");
                OnLMBDown();
                GetComponent<S_WeaponSystem>().GetCurrentWeapon.WeaponShoot();
                StartCoroutine(AnimateCursor360());


                lastRofTime = Time.time;


                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit, 1000))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    //Check what object was hit by the ray:
                    if (hitObject.GetComponent<S_Enemy>())
                    {
                        //Enemy
                        hitObject.GetComponent<S_WaypointMover>().TurnBack();
                        //Call a static event that "SOME" enemy was hit.
                        S_EnemyManager.OnEnemyHit(hit.point, hitObject.GetComponent<S_Enemy>());
                    }
                }
            }
            else
            {
                print("Click. Click.");
            }

        }
    }

    float animTimer = 0;

    IEnumerator AnimateCursor360()
    {
        animTimer = 0;
        cursorImage.fillClockwise = false;
        while(animTimer < GetComponent<S_WeaponSystem>().GetCurrentWeapon.weaponShootInterval/2)
        {
            cursorImage.fillAmount = Mathf.InverseLerp(GetComponent<S_WeaponSystem>().GetCurrentWeapon.weaponShootInterval / 2, 0, animTimer);

            animTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        animTimer = 0;
        cursorImage.fillClockwise = true;
        while (animTimer < GetComponent<S_WeaponSystem>().GetCurrentWeapon.weaponShootInterval / 2)
        {
            cursorImage.fillAmount = Mathf.InverseLerp(0, GetComponent<S_WeaponSystem>().GetCurrentWeapon.weaponShootInterval / 2, animTimer);

            animTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}

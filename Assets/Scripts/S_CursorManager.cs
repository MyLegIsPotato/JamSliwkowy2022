using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class S_CursorManager : MonoBehaviour
{
    public delegate void LMBDownHandler();
    public static LMBDownHandler OnLMBDown;

    public Texture2D cursorDefault;
    [SerializeField]
    private Canvas playerUI;



    private void Start()
    {
        S_EnemyManager.OnEnemyHit += EnemyHitFX;
        S_EnemyManager.OnEnemyHit += EnemyHitSFX;
    }

    void Awake()
    {
        //Cursor.visible = false;
        Cursor.SetCursor(cursorDefault, new Vector2(cursorDefault.width/2, cursorDefault.height/2), CursorMode.ForceSoftware);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnLMBDown();
        }
    }

    public void EnemyHitFX(Vector3 hitLocation, S_Enemy _e)
    {
        GameObject go = Instantiate(GetComponent<S_PlayerManager>().GetCurrentWeapon.hitFX_Prefab);
        go.transform.position = hitLocation;
    }
    public void EnemyHitSFX(Vector3 hitLocation, S_Enemy _e)
    {
        _e.GetComponent<AudioSource>().PlayOneShot(GetComponent<S_PlayerManager>().GetCurrentWeapon.weaponHitSounds[
            UnityEngine.Random.Range(0, GetComponent<S_PlayerManager>().GetCurrentWeapon.weaponHitSounds.Count - 1) //kill me pls
            ]
        );
    }
}

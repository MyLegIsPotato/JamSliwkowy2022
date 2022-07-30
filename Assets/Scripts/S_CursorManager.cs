using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class S_CursorManager : MonoBehaviour
{

    public Texture2D cursorDefault;
    [SerializeField]
    private Canvas playerUI;

    private void Start()
    {
        S_EnemyManager.OnEnemyHit += EnemyHit;
    }

    void Awake()
    {
        //Cursor.visible = false;
        Cursor.SetCursor(cursorDefault, new Vector2(cursorDefault.width/2, cursorDefault.height/2), CursorMode.ForceSoftware);
    }

    public void EnemyHit(Vector3 hitLocation)
    {
        print("Event dzia³a!");
        GameObject go = Instantiate(GetComponent<S_PlayerManager>().currentWeapon.hitFX_Prefab);
        go.transform.position = hitLocation;

    }
}

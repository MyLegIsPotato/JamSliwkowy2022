using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class S_CursorManager : MonoBehaviour
{

    public Texture2D cursorDefault;
    

    void Awake()
    {
        //Cursor.visible = false;
        Cursor.SetCursor(cursorDefault, new Vector2(cursorDefault.width/2, cursorDefault.height/2), CursorMode.ForceSoftware);
    }
}

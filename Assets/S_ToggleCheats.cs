using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ToggleCheats : MonoBehaviour
{
    [SerializeField]
    GameObject cheatsParent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) == true){
            cheatsParent.SetActive(!cheatsParent.activeInHierarchy);
        }
    }
}

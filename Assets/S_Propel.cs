using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Propel : MonoBehaviour
{
    public bool go = false;

    [SerializeField]
    float speed = 4;



    void Update()
    {
       if(go)
            this.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + -speed * Time.deltaTime);
    }
}

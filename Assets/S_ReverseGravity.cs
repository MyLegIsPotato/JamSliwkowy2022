using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ReverseGravity : MonoBehaviour
{
    public
    bool _enabled = true;

    void FixedUpdate()
    {
        if(_enabled)
            GetComponent<Rigidbody>().AddForce(-2*Physics.gravity, ForceMode.Acceleration);
    }
}

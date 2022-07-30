using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class FX_Destructor : MonoBehaviour
{

    private void Update()
    {
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !GetComponent<Animator>().IsInTransition(0))
                Destroy(this.gameObject);
    }
}

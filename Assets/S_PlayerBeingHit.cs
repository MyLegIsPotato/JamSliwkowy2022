using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerBeingHit : MonoBehaviour
{
    [SerializeField]
    S_PlayerManager daddy;

    private void OnCollisionEnter(Collision collision)
    {
        print("I'm being pushed!");

        //play health jump animation
        daddy.GetComponent<S_PlayerManager>().GetHit();
        //play vignette animation
    }

}

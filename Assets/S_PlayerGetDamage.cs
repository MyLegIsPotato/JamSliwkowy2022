using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerGetDamage : MonoBehaviour
{
    [SerializeField]
    Collider playerCollider_1;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            //Play getting hit animations:
                //Screen effect (sanity, health...)

                //Camera wobble (punched, pushed...)

        }
    }
}

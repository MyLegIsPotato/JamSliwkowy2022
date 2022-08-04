using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerBeingHit : MonoBehaviour
{
    [SerializeField]
    S_PlayerManager daddy;

    float hitInterval = 0.5f;
    float lastHitTime = 0;
    private void OnCollisionEnter(Collision collision)
    {
        print("I'm being hit!");
        if(collision.collider.tag == "Enemy")
        {
            if (Time.time > lastHitTime + hitInterval)
            {
                daddy.GetComponent<S_PlayerManager>().GetHit(collision.collider.GetComponent<S_Enemy>().damage);
                lastHitTime = Time.time;
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy_Zombie : S_Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<S_WaypointMover>().onFinish -= base.ArrivedAction;
        GetComponent<S_WaypointMover>().onFinish += this.ArrivedAction;

        //StartCoroutine(KeepScreaming(2));
    }

    public void ScreamOnce()
    {
        GetComponent<AudioSource>().PlayOneShot(attackSFXs[0]);
    }

    IEnumerator KeepScreaming(float s)
    {
        while (true)
        {
            ScreamOnce();
            yield return new WaitForSeconds(s);
        }
    }

    public override void ArrivedAction(Transform waypoint)
    {
        StartCoroutine(AttackProcedure());
    }

    IEnumerator AttackProcedure()
    {
        ScreamOnce();
        GetComponentInChildren<Animator>().SetTrigger("Taunt");
        yield return new WaitForSeconds(3f);
       // GetComponentInChildren<Animator>().SetTrigger("Attack"); Attack plays automatically from animator


    }

   
}

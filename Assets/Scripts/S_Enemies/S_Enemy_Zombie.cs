using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy_Zombie : S_Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KeepScreaming(2));
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
}

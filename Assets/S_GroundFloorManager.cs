using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GroundFloorManager : MonoBehaviour
{
    public Light red1;
    public Light red2;
    public Light blue1;
    public Light blue2;

    private void Start()
    {
        S_ElevatorController.OnElevatorArrived += (x) => { if (GetComponent<S_FloorNumber>().thisFloorNum == x) StartCoroutine(AnimatePolice()); };
    }

    IEnumerator AnimatePolice()
    {
        yield return new WaitForSeconds(1);
        GetComponentInChildren<Animator>().SetTrigger("Go");
        GetComponentInChildren<AudioSource>().Play();
        print("podjecha³y pa³y koniec zabawy");
        StartCoroutine(AnimateLights());
        yield return new WaitForSeconds(5);
        FindObjectOfType<S_ElevatorController>().StartCoroutine
            (FindObjectOfType<S_ElevatorController>().MoveToFloor(GetComponent<S_FloorNumber>().thisFloorNum + 1));
        yield return new WaitForSeconds(15);
        GetComponentInChildren<AudioSource>().Stop();
        StopCoroutine(AnimateLights());
        blue1.enabled = false;
        blue2.enabled = false;
        red1.enabled = false;
        red2.enabled = false;
        FindObjectOfType<S_MusicSelector>().PlayCaveMusic();
        yield return null;
    }

    IEnumerator AnimateLights()
    {
        while (true)
        {
            red1.enabled = true;
            yield return new WaitForSeconds(0.1f);
            blue1.enabled = false;
            yield return new WaitForSeconds(0.1f);
            red1.enabled = false;
            blue1.enabled = true;
            yield return new WaitForSeconds(0.1f);
            red2.enabled = true;
            yield return new WaitForSeconds(0.1f);
            blue2.enabled = false;
            yield return new WaitForSeconds(0.1f);
            red2.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

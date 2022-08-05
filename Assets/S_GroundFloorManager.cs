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

        S_ElevatorController.OnElevatorArrived += (x) => { 
            if (GetComponent<S_FloorNumber>().thisFloorNum == x)
            {
                if (!S_GameManager.GameLost)
                    StartCoroutine(AnimatePolice());
                else
                    StartCoroutine(AnimateTheEnd());
            }

        };
    }

    IEnumerator AnimateTheEnd()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<S_PlayerManager>().GetComponent<Animator>().enabled = true;
        FindObjectOfType<S_PlayerManager>().GetComponent<Animator>().SetTrigger("Leave");
    }

    IEnumerator AnimatePolice()
    {
        yield return new WaitForSeconds(1);
        GetComponentInChildren<Animator>().SetTrigger("Go");
        GetComponentInChildren<AudioSource>().Play();
        print("podjecha³y pa³y koniec zabawy");
        StartCoroutine(AnimateLights());

        FindObjectOfType<S_PlayerManager>().GetComponent<Animator>().enabled = true;
        FindObjectOfType<S_PlayerManager>().GetComponent<Animator>().SetTrigger("Glitch1");
        yield return new WaitForSeconds(4);


        FindObjectOfType<S_ElevatorController>().normalElevator.SetActive(false);
        FindObjectOfType<S_ElevatorController>().mineElevator.SetActive(true);

        yield return new WaitForSeconds(10);
        FindObjectOfType<S_PlayerManager>().GetComponent<Animator>().enabled = false;
        GetComponentInChildren<AudioSource>().Stop();
        FindObjectOfType<S_ElevatorController>().StartCoroutine(FindObjectOfType<S_ElevatorController>().MoveToFloor(GetComponent<S_FloorNumber>().thisFloorNum + 1));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EndFloorManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioClip endMusic;
    [SerializeField]
    Animator dollyanimator;

    void Start()
    {
        S_ElevatorController.OnElevatorArrived += (x) => {
            if (GetComponent<S_FloorNumber>().thisFloorNum == x)
            {
                    StartCoroutine(ShowEndingCutScene());
            }
        };
    }

    IEnumerator ShowEndingCutScene()
    {
        Camera.main.depth = -1;
        FindObjectOfType<Canvas>().gameObject.SetActive(false);
        FindObjectOfType<S_PlayerManager>().GetComponent<AudioSource>().PlayOneShot(endMusic);
        dollyanimator.enabled = true;
        FindObjectOfType<S_MusicSelector>().StopMusic();

        yield return new WaitForSeconds(25-8f);
        FindObjectOfType<S_ElevatorController>().StartCoroutine(FindObjectOfType<S_ElevatorController>().MoveToNextFloor());
        FindObjectOfType<S_ElevatorController>().StartCoroutine(FindObjectOfType<S_ElevatorController>().OperateDoors(false));
        yield return new WaitForSeconds(8f);


        Camera.main.depth = 1;


        yield return null;
    }
}

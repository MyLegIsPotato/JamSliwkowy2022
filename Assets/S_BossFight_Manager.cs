using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BossFight_Manager : MonoBehaviour
{
    private void Start()
    {
        S_ElevatorController.OnElevatorArrived += (floor) => { StartBossFight(floor); };
    }

    void StartBossFight(int floorNum)
    {
        if (GetComponent<S_FloorNumber>().thisFloorNum == floorNum)
            StartCoroutine(BossFight());
    }

    IEnumerator BossFight()
    {
        print("Boss Fight beginning...");
        //animations:
        //boss get up

        //boss shout
        //boss throw
        //boss show HP bar
        //boss start walking

        yield return null;
    }
}

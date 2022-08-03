using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BossFight_Manager : MonoBehaviour
{
    S_Enemy_Boss boss;

    private void Start()
    {
        S_ElevatorController.OnElevatorArrived += (floor) => { StartBossFight(floor); };
        boss = GetComponentInChildren<S_Enemy_Boss>();
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
        GameObject bossBar = FindObjectOfType<S_UI_Animator>(true).bossHPBar;
        boss.bossHPBarImage = bossBar.GetComponent<UnityEngine.UI.Image>();
        GetComponentInChildren<S_Enemy_Boss>().CheckWP(null);
        //music ON
        yield return new WaitForSeconds(4f);
        FindObjectOfType<S_MusicSelector>().PlayBossMusic();   
        bossBar.SetActive(true);
       

        //boss shout
        //boss throw
        //boss show HP bar
        //boss start walking

        yield return null;
    }
}

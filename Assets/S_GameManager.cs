using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameManager : MonoBehaviour
{
    public delegate void GameStartHandler();
    public static GameStartHandler OnGameStarted;


    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("Game Running");        
    }

    public void StartGame()
    {
        Debug.LogWarning("Game Starting...");
        OnGameStarted();
        Debug.LogWarning("Game Started.");
    }


    public void SkipTo(int floor)
    {
        Debug.LogWarning("Game Skipped Starting...");
        GetComponent<S_ElevatorController>().StartCoroutine(
            GetComponent<S_ElevatorController>().MoveToFloor(floor)
        );
        Debug.LogWarning("Game Skipped Started.");
    }
}

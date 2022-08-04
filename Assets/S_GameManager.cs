using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void SkipTo(Transform waypoint)
    {
        
        Debug.LogWarning("Game Skipped Starting...");
        GetComponent<S_ElevatorController>().StartCoroutine(
            GetComponent<S_ElevatorController>().MoveToFloor(waypoint.GetSiblingIndex())
        );
        Debug.LogWarning("Game Skipped Started.");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

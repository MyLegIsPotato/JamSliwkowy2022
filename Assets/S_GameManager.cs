using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameManager : MonoBehaviour
{
    public delegate void GameStartHandler();
    public static GameStartHandler OnGameStarted;

    public static bool GameLost = false;
    public static bool gameStarted = false;


    void OnDisable()
    {
        OnGameStarted = null;
    }


    // Start is called before the first frame update
    void Start()
    {
        GameLost = false;
        
        Debug.LogWarning("Game Running");        
    }

    public void StartGame()
    {
        Debug.LogWarning("Game Starting...");
        OnGameStarted();
        Debug.LogWarning("Game Started.");
        gameStarted = true;
    }


    public void SkipTo(Transform waypoint)
    {
        FindObjectOfType<S_PlayerManager>().CurrentPOINTS = FindObjectOfType<S_PlayerManager>().maxPOINTS;

        Debug.LogWarning("Game Skipped Starting...");
        GetComponent<S_ElevatorController>().StartCoroutine(
            GetComponent<S_ElevatorController>().MoveToFloor(waypoint.GetSiblingIndex())
        );
        Debug.LogWarning("Game Skipped Started.");
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

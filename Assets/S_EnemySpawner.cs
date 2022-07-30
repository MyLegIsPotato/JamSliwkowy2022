using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    public S_Waypoints waypoints;

    private void Start()
    {
        waypoints = GetComponentInChildren<S_Waypoints>();
    }

    internal void SpawnEnemy(GameObject enemyToSpawn)
    {
        GameObject _e = Instantiate(enemyToSpawn, this.transform);
        _e.transform.localPosition = Vector3.zero;
        _e.GetComponent<S_WaypointMover>().waypoints = waypoints;
    }
}

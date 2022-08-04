using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    public S_Waypoints waypoints;
    GameObject myEnemy;

    private void Start()
    {
        waypoints = GetComponentInChildren<S_Waypoints>();
        //print("halko");
    }

    internal void SpawnEnemy(GameObject enemyToSpawn)
    {
        myEnemy = Instantiate(enemyToSpawn, this.transform);
        myEnemy.transform.localPosition = Vector3.zero;
        myEnemy.GetComponent<S_WaypointMover>().waypoints = waypoints;
        

    }

    public void Release()
    {
        print("Releasing... ");
        S_EnemyManager.spawnersBusy[this] = false;
    }
}

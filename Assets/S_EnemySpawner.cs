using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{

    internal void SpawnEnemy(GameObject enemyToSpawn)
    {
        GameObject _e = Instantiate(enemyToSpawn, this.transform);
        _e.transform.localPosition = Vector3.zero;
    }
}

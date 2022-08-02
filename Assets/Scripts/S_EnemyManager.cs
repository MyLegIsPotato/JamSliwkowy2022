using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyManager : MonoBehaviour
{
    public int enemiesToSpawn = 6;
    public float spawnInteval = 1;

    [SerializeField]
    List<S_EnemySpawner> spawners;

    [SerializeField]
    public List<GameObject> enemyPrefabs;

    public delegate void EnemyHitHandler(Vector3 hitPosition, S_Enemy hitEnemy, S_Weapon usedWeapon);
    public static EnemyHitHandler OnEnemyHit;


    public delegate void EnemyDeathHandler();
    public static EnemyDeathHandler OnEnemyDeath;

    public static int enemiesAlive = 0;

    public void Start()
    {
        OnEnemyDeath += () => { };
        S_ElevatorController.OnElevatorArrived += (x) => { if (GetComponentInParent<S_FloorNumber>().thisFloorNum == x) SpawnEnemies(); };
        //StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        //wait before round starts
        yield return new WaitForSeconds(2f);


        for (int i = 0; i < enemiesToSpawn; i++)
        {
            spawners[Random.Range(0, spawners.Count-1)].SpawnEnemy(enemyPrefabs[0]);
            enemiesAlive++;
            yield return new WaitForSeconds(spawnInteval);
        }
        yield return null;
    }

    public void SpawnEnemies()
    {
        StartCoroutine(spawnEnemies());
    }
}

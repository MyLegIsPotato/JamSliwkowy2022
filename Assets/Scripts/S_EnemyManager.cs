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


    public delegate void EnemyDeathHandler(S_Enemy deadEnemy);
    public static EnemyDeathHandler OnEnemyDeath;

    public static int enemiesAlive = 0;

    public bool oneForEachLine = false;
    public static Dictionary<S_EnemySpawner, bool> spawnersBusy;

    public void Start()
    {
        OnEnemyDeath += (e) => { };
        S_ElevatorController.OnElevatorArrived += (x) => { if (GetComponentInParent<S_FloorNumber>().thisFloorNum == x) SpawnEnemies(); };
        //StartCoroutine(spawnEnemies());
        CreateDictOfLastWaypoints();
    }

    public void CreateDictOfLastWaypoints()
    {
        spawnersBusy = new Dictionary<S_EnemySpawner, bool>();
        foreach (S_EnemySpawner spawner in GetComponentsInChildren<S_EnemySpawner>())
        {
            //Get last waypoint of each waypoint tree:
            spawnersBusy.Add(spawner, false);
        }
    }

    IEnumerator spawnEnemies()
    {
        //wait before round starts
        yield return new WaitForSeconds(2f);


        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (oneForEachLine)
            {
                int x;
                bool isBusy;
                do
                {
                    x = Random.Range(0, spawners.Count);
                    spawnersBusy.TryGetValue(spawners[x], out isBusy);
                    print("Trying to spawn at: " + x);
                    yield return null;
                } while (isBusy);

                print("Success! Spawning at " + x);
                spawners[x].SpawnEnemy(enemyPrefabs[0]);
                spawnersBusy[spawners[x]] = true;
                
            }
            else
            {
                spawners[Random.Range(0, spawners.Count - 1)].SpawnEnemy(enemyPrefabs[0]);
            }
            
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

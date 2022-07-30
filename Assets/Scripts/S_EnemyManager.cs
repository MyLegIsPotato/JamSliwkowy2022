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


    public void Start()
    {
        print("Spawning");

       
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        //wait before round starts
        yield return new WaitForSeconds(2f);


        for (int i = 0; i < enemiesToSpawn; i++)
        {
            spawners[Random.Range(0, spawners.Count-1)].SpawnEnemy(enemyPrefabs[0]);
            yield return new WaitForSeconds(spawnInteval);
        }


        yield return null;
    }
}

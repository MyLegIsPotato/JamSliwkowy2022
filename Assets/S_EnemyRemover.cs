using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyRemover : MonoBehaviour
{
    public static S_EnemyRemover i;
    void Start()
    {
        i = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            RemoveEnemy(other.gameObject);

        }
        
    }

    public void RemoveEnemy(GameObject enemy)
    {
        S_EnemyManager.enemiesAlive--;
        enemy.GetComponent<S_WaypointMover>().StopAllCoroutines();
        S_EnemyManager.OnEnemyDeath();
        
        StartCoroutine(RemoveEnemyObject(enemy.GetComponent<S_Enemy>()));
    }

    IEnumerator RemoveEnemyObject(S_Enemy e)
    {
        yield return new WaitForSeconds(e.bodyDespawnTime);
        Destroy(e);
    }
}

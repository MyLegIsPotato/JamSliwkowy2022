using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            RemoveEnemy(other.gameObject);
        }
    }

    public static void RemoveEnemy(GameObject enemy)
    {
        S_EnemyManager.enemiesAlive--;
        enemy.GetComponent<S_WaypointMover>().StopAllCoroutines();
        S_EnemyManager.OnEnemyDeath();
        Destroy(enemy);
    }
}

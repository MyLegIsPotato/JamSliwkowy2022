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
            RemoveEnemy(other.gameObject.GetComponent<S_Enemy>());
        }
        
    }

    public void RemoveEnemy(S_Enemy enemy)
    {
        enemy.GetComponentInParent<S_EnemyManager>().enemiesAlive--;
        enemy.GetComponentInParent<S_EnemyManager>().enemiesKilled++;
        S_EnemyManager.OnEnemyDeath();

        enemy.GetComponent<S_WaypointMover>().StopAllCoroutines(); 
        StartCoroutine(RemoveEnemyObject(enemy));
    }

    IEnumerator RemoveEnemyObject(S_Enemy e)
    {
        yield return new WaitForSeconds(e.bodyDespawnTime);
        if(e != null)
            Destroy(e.gameObject);
        yield return null;
    }
}

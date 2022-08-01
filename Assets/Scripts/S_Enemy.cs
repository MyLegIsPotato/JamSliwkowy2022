using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy : MonoBehaviour
{

    private int health;
    public int Health
    {
        get { return health; } 
        set { 
            health = value; 
            if(health == 0)
            {
                print("I'm " + this.gameObject.name + " dead!");
                S_EnemyManager.OnEnemyDeath();
            }
        }
    }

    public int damage;

    public AudioClip moveSFX;
    public AudioClip attackSFX;

    public void Start()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        if (source != null)
        {
            source.clip = moveSFX;
            source.loop = true;
            source.spatialBlend = 1f;
            source.Play();
        }
        GetComponent<S_WaypointMover>().onFinish += ArrivedAction;
    }

    private void ArrivedAction() 
    {
        print("Just arrived");
        GetComponentInParent<S_EnemyManager>().SpawnSomeEnemies(1);
        S_EnemyRemover.RemoveEnemy(gameObject);
    }
}

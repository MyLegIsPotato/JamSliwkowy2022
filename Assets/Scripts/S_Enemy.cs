using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy : MonoBehaviour
{
    public float POINTS_Multiplier = 1;

    [SerializeField]
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

    [SerializeField]
    private int happiness;
    public int Happiness
    {
        get { return happiness; }
        set
        {
            happiness = value;
            if (happiness == 0)
            {
                print("I'm " + this.gameObject.name + " depressed!");
                S_EnemyManager.OnEnemyDeath();
            }
        }
    }

    public int damage;

    public AudioClip moveSFX;
    public List<AudioClip> attackSFXs;

    //Health Damage
    public GameObject hitFX_Prefab;
    public List<AudioClip> hitSFXs;

    //Emotional Damage
    public GameObject emo_hitFX_Prefab;
    public List<AudioClip> emo_hitSFXs;

    void Start()
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

    private void ArrivedAction(Transform waypoint) 
    {
        print("Just arrived");
        S_EnemyRemover.RemoveEnemy(gameObject);
    }

    public virtual void Attack()
    {
        print("Attacking!");
    }

    public virtual void Evade()
    {
        print("Evading cursor!");
    }

    public virtual void EnemyReaction()
    {
        print("I'm hit");
    }

    public virtual void Hit(Vector3 hitLocation, S_Weapon _wp)
    {
        if(_wp.weaponDamage > 0)
        {
            //Do health damage
            Health += _wp.weaponDamage;

            //Spawn sprite FX
            GameObject go = Instantiate(hitFX_Prefab);
            go.transform.position = hitLocation;

            //Play sound FX
            GetComponent<AudioSource>().PlayOneShot(hitSFXs[Random.Range(0, hitSFXs.Count - 1)]);
            //Call a static event that "SOME" enemy was hit.

            S_EnemyManager.OnEnemyHit(hitLocation, this, _wp);
        }
        else if (_wp.emotionalDamage > 0)
        {
            Happiness += _wp.emotionalDamage;

            //Spawn sprite FX
            GameObject go = Instantiate(emo_hitFX_Prefab);
            go.transform.position = hitLocation;

            //Play sound FX
            GetComponent<AudioSource>().PlayOneShot(emo_hitSFXs[Random.Range(0, emo_hitSFXs.Count - 1)]);
            S_EnemyManager.OnEnemyHit(hitLocation, this, _wp);

        }

        EnemyReaction();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy : MonoBehaviour
{

    private int health;
    public int Health
    {
        get { return health; } 
        set { health = value; }
    }

    public int damage;

    public AudioClip moveSFX;
    public AudioClip attackSFX;

    public void Start()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        if (source != null)
        {
            source.clip = moveSFX;
            source.loop = true;
            source.spatialBlend = 1f;
            source.Play();
        }
    }

    private void OnMouseDown()
    {
        this.GetComponent<S_WaypointMover>().TurnBack();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000))
        {
            print("Ouch! That Ray hit me, " + this.name);
        }

        //Call a static event that "SOME" enemy was hit.
        S_EnemyManager.OnEnemyHit(hit.point);
    }
}

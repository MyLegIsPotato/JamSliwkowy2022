using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy_Boss : S_Enemy
{

    [SerializeField]
    public AudioClip rageSound;

    public Transform deskWaypoint;
    public Transform safeWaypoint;
    public Transform elevatorWaypoint;
    public Transform shelfWaypoint1;
    public Transform shelfWaypoint2;
    public Transform rageWaypoint;

    public GameObject bulletPrefab;

    [SerializeField]
    Transform rightHand;


   
    public
    UnityEngine.UI.Image bossHPBarImage;


    public void Start()
    {
        GetComponent<S_WaypointMover>().onArrive += CheckWP;
        base.maxHealth = 1000; //To Do Remove
    }


    public override void Attack()
    {
        base.Attack();
        GetComponent<Animator>().SetTrigger("Kick"); //-> Damage player from animation event
        transform.LookAt(Camera.main.transform);
        transform.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    [SerializeField]
    Animator table;

    public override void EnemyReaction()
    {
        base.EnemyReaction();
        if(health < maxHealth / 2)
        {
            print("---- SECOND PHASE! ----");
            print("---- SECOND PHASE! ----");
            print("---- SECOND PHASE! ----");

            GetComponent<S_WaypointMover>().altOnJunction = true;
        }
    }

    public override IEnumerator DieProcess()
    {
        print("I'm " + this.gameObject.name + " dead!");
        //S_EnemyManager.OnEnemyDeath();

        GetComponent<S_WaypointMover>().keepMoving = false;
        GetComponent<S_WaypointMover>().autoProceed = false;
        yield return new WaitForSeconds(0.05f);
        GetComponent<Animator>().applyRootMotion = true;
        GetComponent<Animator>().SetTrigger("Die");
        S_EnemyRemover.i.RemoveEnemy(this);
        yield return new WaitForSeconds(3f);
        FindObjectOfType<S_MusicSelector>().PlayDefaultMusic();
        yield return null;
    }

    public void KickTable()
    {
        table.SetTrigger("Kick");
    }

    public void DamagePlayer()
    {
        GameObject.FindObjectOfType<S_PlayerManager>().CrackScreen();
    }

    public void Throw()
    {
        
        transform.LookAt(Camera.main.transform);
        Instantiate(bulletPrefab, rightHand.position, Quaternion.identity);
    }

    public void CheckWP(Transform waypoint)
    {
        StartCoroutine(waypointProcedure(waypoint));
    }

    bool tableKicked = false;

    IEnumerator waypointProcedure(Transform waypoint)
    {
        while(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Get Up"))
        {
            yield return new WaitForEndOfFrame();
        }

        switch (waypoint)
        {
            case var expression when waypoint == null: //Initate Fight
                GetComponent<Animator>().SetTrigger("GetUp");
                yield return new WaitForSeconds(3);
                GetComponent<S_WaypointMover>().ProceedToIndex(0);
                S_EnemyManager.OnEnemyHit += (x, y, z) => { bossHPBarImage.fillAmount = Mathf.InverseLerp(0, base.maxHealth, Health); };
                tableKicked = false;

                break;

            case var expression when waypoint == deskWaypoint:
                Debug.Log("Throw Statue");
                GetComponent<Animator>().SetTrigger("Throw");
                yield return new WaitForSeconds(4f);
                Debug.Log("Throw Statue");
                GetComponent<Animator>().SetTrigger("Throw");
                yield return new WaitForSeconds(2f);
                GetComponent<S_WaypointMover>().ProceedToNext();

                break;
            case var expression when waypoint == safeWaypoint:
                Debug.Log("Throw Money");
                GetComponent<Animator>().SetTrigger("Throw");
                yield return new WaitForSeconds(2f);
                GetComponent<Animator>().SetTrigger("Throw");
                yield return new WaitForSeconds(4f);
                GetComponent<Animator>().SetTrigger("Throw");
                yield return new WaitForSeconds(2f);
                GetComponent<S_WaypointMover>().ProceedToNext();

                break;
            case var expression when waypoint == elevatorWaypoint:
                Debug.Log("Kick!");
                Attack();
                yield return new WaitForSeconds(2f);
                GetComponent<S_WaypointMover>().ProceedToNext();

                break;
            case var expression when waypoint == shelfWaypoint1:
                Debug.Log("Throw something!");
                yield return new WaitForSeconds(4f);
                GetComponent<Animator>().SetTrigger("Throw");
                GetComponent<S_WaypointMover>().ProceedToNext();
                break;
            case var expression when waypoint == shelfWaypoint2:
                Debug.Log("Throw something else!");
                yield return new WaitForSeconds(4f);
                GetComponent<Animator>().SetTrigger("Throw");
                GetComponent<S_WaypointMover>().ProceedToNext();

                break;
            case var expression when waypoint == rageWaypoint:
                Debug.Log("RAAGEE");
                GetComponent<Animator>().SetTrigger("Taunt");
                GetComponent<AudioSource>().PlayOneShot(rageSound);
                yield return new WaitForSeconds(4f);
               

                if (!tableKicked)
                {
                    Debug.Log("Kick Table!");
                    GetComponent<Animator>().SetTrigger("KickTable");
                    transform.LookAt(Camera.main.transform);
                    transform.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
                }
                yield return new WaitForSeconds(2f);
                GetComponent<S_WaypointMover>().ProceedToNext();

                break;
            default:
                print("Normal waypoint");
                GetComponent<S_WaypointMover>().ProceedToNext();
                break;
        }

        yield return null;
    }

}

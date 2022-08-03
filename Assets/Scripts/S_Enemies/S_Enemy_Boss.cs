using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy_Boss : S_Enemy
{
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
                Debug.Log("Kick!");
                Attack();
                break;
            default:
                print("Normal waypoint");
                GetComponent<S_WaypointMover>().ProceedToNext();
                break;
        }

        yield return null;
    }

}

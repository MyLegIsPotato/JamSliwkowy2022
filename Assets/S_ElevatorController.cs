using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class S_ElevatorController : MonoBehaviour
{
    [SerializeField]
    GameObject LeftDoors;
    [SerializeField]
    GameObject RightDoors;

    public AnimationCurve doorsPosition;

    [Range(0f, 1f)]
    public float doorOpenessFactor;

    public int floorNum = 7; //Start from 7 going down
    public float destinationHeight;

    public bool isMoving = true;

    public delegate void ElevatorMovementHandler();
    public static ElevatorMovementHandler OnElevatorArrived;
    public static ElevatorMovementHandler OnElevatorDeparted;

    public float timeBetweenDingAndOpeningDoors = 2f;

    public AudioClip elevatorDing;

    public AudioSource elevatorAmbientEmitter;
    public AudioSource elevatorDoorSound;


    public void Start()
    {
        S_GameManager.OnGameStarted += () => { StartCoroutine(MoveToNextFloor()); };


        //S_EnemyManager.OnEnemyDeath += MoveIfEnemiesDead;
        //GetComponent<S_WaypointMover>().onArrive += () => { OnElevatorArrived(); };
        //GetComponent<S_WaypointMover>().onDepart += () => { OnElevatorDeparted(); };

        //OnElevatorArrived += () => { print("Arriving!"); };
        //OnElevatorArrived += PlayDing;

        //OnElevatorDeparted += () => { print("Departing!"); };
    }

    private void Update()
    {
        LeftDoors.transform.localPosition = new Vector3(-doorsPosition.Evaluate(doorOpenessFactor), 0, 0);
        RightDoors.transform.localPosition = new Vector3(doorsPosition.Evaluate(doorOpenessFactor), 0, 0);
    }
    IEnumerator MoveToNextFloor()
    {
        floorNum--;
        yield return StartCoroutine(OperateDoors(true));

        print("door closed");

        yield return null;
    }

    public void MoveIfEnemiesDead()
    {
        print("Enemies alive is still: " + S_EnemyManager.enemiesAlive);
        if (S_EnemyManager.enemiesAlive == 0)
            StartCoroutine(MoveToNextFloor());
    }

    public void PlayDing()
    { 
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = elevatorDing;
        source.Play();
    }

    IEnumerator OperateDoors(bool close)
    {
        elevatorDoorSound.Play();

        if (close)
        {
            GetComponent<Animator>().SetTrigger("Close");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Open");
        }

        yield return new WaitForSeconds(1.5f);

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DoNothing"))
        {
            print("do nothing");
            yield return null;
        }
        else
        {
            print("doing something");
            yield return new WaitForEndOfFrame();
        }
    }
}

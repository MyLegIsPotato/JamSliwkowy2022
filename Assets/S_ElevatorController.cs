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

    [SerializeField]
    public float travelTime = 10;

    [Header("Floor Numeration")]
    [SerializeField]
    List<Sprite> floorDisplaySprites;
    [SerializeField]
    GameObject floorDisplayOfInsanity;
    [SerializeField]
    UnityEngine.UI.Image floorDisplayObject;

    private int floorNum = 7;
    public int FloorNum
    {
        get { return floorNum; }
        set
        {
            floorNum = value;
        }
    }

   

    public float destinationHeight;

    public bool isMoving = true;

    public delegate void ElevatorMovementHandler(int floorNum);
    public static ElevatorMovementHandler OnElevatorArrived;
    public static ElevatorMovementHandler OnElevatorDeparted;

    public float timeBetweenDingAndOpeningDoors = 2f;

    public AudioClip elevatorDing;

    public AudioSource elevatorAmbientEmitter;
    public AudioSource elevatorDoorSound;

    [Header("Floors")]
    [SerializeField]
    public S_Waypoints FloorWaypoints;
    private List<S_FloorNumber> floors;

    public int numberOfFloors;


    public void Start()
    {
        numberOfFloors = FloorWaypoints.transform.childCount;
        floorNum = 0;

        S_EnemyManager.OnEnemyDeath += (e) => { MoveIfEnemiesDead(); };
        S_GameManager.OnGameStarted += () => { StartCoroutine(MoveToNextFloor()); };

        foreach(Transform t in FloorWaypoints.transform)
        {
            t.GetComponentInChildren<S_FloorNumber>().thisFloorNum = t.GetSiblingIndex();
        }
    }

    private void Update()
    {
        LeftDoors.transform.localPosition = new Vector3(-doorsPosition.Evaluate(doorOpenessFactor), 0, 0);
        RightDoors.transform.localPosition = new Vector3(doorsPosition.Evaluate(doorOpenessFactor), 0, 0);
    }
    IEnumerator MoveToNextFloor()
    {
        StartCoroutine(MoveToFloor(floorNum+1));
        yield return null;
    }
    public IEnumerator MoveToFloor(int floorToSkipTo)
    {

        yield return StartCoroutine(OperateDoors(true)); //Close the doors
        OnElevatorDeparted(FloorNum);

        do
        {
            FloorNum++;
            GetComponent<S_WaypointMover>().ProceedToNext();
            print("Waiting...");
            yield return new WaitForSeconds(0.4f);
            elevatorAmbientEmitter.Play();

            yield return new WaitForSeconds(travelTime);
            ChangeFloorDisplayNumber();
        }
        while (FloorNum < floorToSkipTo);


        print("Wait Complete.");
        elevatorAmbientEmitter.Stop();

        OnElevatorArrived(FloorNum);
        StartCoroutine(OperateDoors(false));

        yield return null;
    }

    public void ChangeFloorDisplayNumber()
    {
        if (floorNum == 0)
        {
            floorDisplayObject.sprite = null;
        }
        else
        {
            if (floorNum < 6)
                floorDisplayObject.sprite = floorDisplaySprites[6 - floorNum];
            else
                floorDisplayOfInsanity.SetActive(true);
        }
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
        if (!close)
            PlayDing(); //play sound before opening the doors
        yield return new WaitForSeconds(2f);

        elevatorDoorSound.Play();

        Animator anim = GetComponent<Animator>();
        if (close)
        {
            anim.SetTrigger("Close");
        }
        else
        {
            anim.SetTrigger("Open");
        }

        yield return new WaitForSeconds(6f); //Wait for animation to end.
    }
}

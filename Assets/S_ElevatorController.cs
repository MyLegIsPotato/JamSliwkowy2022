using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class S_ElevatorController : MonoBehaviour
{
    [SerializeField]
    GameObject LeftDoors;
    [SerializeField]
    GameObject RightDoors;

    [SerializeField]
    GameObject LeftDoors_Mine;
    [SerializeField]
    GameObject RightDoors_Mine;

    [SerializeField]
    public GameObject normalElevator;
    [SerializeField]
    public GameObject mineElevator;

    public AnimationCurve doorsPosition;
    public AnimationCurve doorsRotation;


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

    private void OnDisable()
    {
        OnElevatorArrived = null;
        OnElevatorDeparted = null;
    }

    public void Start()
    {
        floors = new List<S_FloorNumber>();
        numberOfFloors = FloorWaypoints.transform.childCount;
        floorNum = 0;

        S_EnemyManager.OnEnemyDeath += () => { MoveIfEnemiesDead(); };
        S_GameManager.OnGameStarted += () => { StartCoroutine(MoveToNextFloor()); };

        foreach(Transform t in FloorWaypoints.transform)
        {
            t.GetComponentInChildren<S_FloorNumber>().thisFloorNum = t.GetSiblingIndex();
            floors.Add(t.GetComponentInChildren<S_FloorNumber>());
        }
    }

    private void Update()
    {
        LeftDoors.transform.localPosition = new Vector3(-doorsPosition.Evaluate(doorOpenessFactor), 0, 0);
        RightDoors.transform.localPosition = new Vector3(doorsPosition.Evaluate(doorOpenessFactor), 0, 0);
        RightDoors_Mine.transform.eulerAngles = new Vector3(0, doorsRotation.Evaluate(doorOpenessFactor), 0);
        LeftDoors_Mine.transform.eulerAngles = new Vector3(0, -doorsRotation.Evaluate(doorOpenessFactor), 0);

    }
    public IEnumerator MoveToNextFloor()
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


            if (FloorNum == 5 && FindObjectOfType<S_PlayerManager>().CurrentPOINTS < FindObjectOfType<S_PlayerManager>().maxPOINTS * 0.8f)
            {
                S_GameManager.GameLost = true;
                StopAllCoroutines();
                StartCoroutine(MoveToNextFloor());
                yield break;
            }

            yield return null;

            if (FloorNum == 6) //Ground Floor
            {
                print("On the Ground Floor");
                if (S_GameManager.GameLost == true)
                {
                    //End Game - Restart;
                    Debug.LogWarning("Game is lost, restarting soon...");
                }
            }

            if (FloorNum == 12)
            {
                doorOpenessFactor = 1;
                GetComponent<S_WaypointMover>().moveSpeed = 2f;
                StopAllCoroutines();
                yield break;
                //StartCoroutine(OperateDoors(false));
            }
        }
        while (FloorNum < floorToSkipTo);



        print("Wait Complete.");
        elevatorAmbientEmitter.Stop();

        OnElevatorArrived(FloorNum);
        StartCoroutine(OperateDoors(false)); //Open the door

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
            else // num > 6
            {
                try
                {
                    floorDisplayOfInsanity.SetActive(true);
                    floorDisplayObject.sprite = floorDisplaySprites[floorNum - 6];
                }
                catch (Exception)
                {

                    
                }

            }
        }
    }


    public void MoveIfEnemiesDead()
    {
        //print("Enemies alive is still: " + floors[floorNum].GetComponentInChildren<S_EnemyManager>().enemiesAlive);
        if (floors[floorNum].GetComponentInChildren<S_EnemyManager>() != null)
        {
            if (floors[floorNum].GetComponentInChildren<S_EnemyManager>().enemiesKilled >= floors[floorNum].GetComponentInChildren<S_EnemyManager>().enemiesToSpawn)
                StartCoroutine(MoveToNextFloor());
        }

    }

    public void PlayDing()
    { 
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = elevatorDing;
        source.Play();
    }

    public IEnumerator OperateDoors(bool close)
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

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
    public float animationTime;

    public int floorNum = 0; //Higher number = higher level = lower floor (we are going down! ;)
    public float destinationHeight;

    int tweenAnim;

    public bool isMoving = true;

    public delegate void ElevatorMovementHandler();
    public static ElevatorMovementHandler OnElevatorArrived;

    public float timeBetweenDingAndOpeningDoors = 2f;

    public AudioClip elevatorDing;
    public AudioClip elevatorMoving;

    public void Start()
    {
        //Start gameplay!
        MoveToNextFloor();
    }

    public void MoveToNextFloor()
    {
        if (floorNum == 0)
        {
            floorNum++;
            destinationHeight = 0f;
            //tweenAnim = LeanTween.moveY(gameObject, destinationHeight, 5f).setLoopOnce().id; //move to y = 0
            print("Going down to floorNum: " + floorNum);
            GetComponent<S_WaypointMover>().onArrive += () => { StartCoroutine(OperateDoors(false)); };
            GetComponent<S_WaypointMover>().onArrive += PlayDing;
        }

    }

    public void PlayDing()
    { 
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = elevatorDing;
        source.Play();
    }

    IEnumerator OperateDoors(bool closed)
    {
        yield return new WaitForSeconds(timeBetweenDingAndOpeningDoors);

        GetComponent<Animator>().SetBool("Closed", closed);

        while (true)
        {
            LeftDoors.transform.localPosition = new Vector3(-doorsPosition.Evaluate(animationTime), 0, 0);
            RightDoors.transform.localPosition = new Vector3(doorsPosition.Evaluate(animationTime), 0, 0);

            yield return new WaitForEndOfFrame();

            if (closed) //Close the door
            {
                if (animationTime >= 1)
                    yield return null; //end coroutine

            }
            else //Open the door
            {
                if (animationTime <= 0)
                    yield return null; //end coroutine
            }

            //if Animation is playing then repeat loop = keep playing evaluating curves.
        }
    }
}

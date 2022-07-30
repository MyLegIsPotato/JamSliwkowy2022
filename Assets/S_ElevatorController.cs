using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ElevatorController : MonoBehaviour
{
    [SerializeField]
    GameObject LeftDoors;
    [SerializeField]
    GameObject RightDoors;

    public AnimationCurve doorsPosition;

    [Range(0f, 1f)]
    public float animationTime;

    public void Start()
    {
    }


    public void Update()
    {
        LeftDoors.transform.position = new Vector3(-doorsPosition.Evaluate(animationTime), 0, 0);
        RightDoors.transform.position = new Vector3(doorsPosition.Evaluate(animationTime), 0, 0);
    }
}

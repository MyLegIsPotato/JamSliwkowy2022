using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Ducking : MonoBehaviour
{
    [Range(1,5f)]
    [SerializeField]
    public float duckSpeed = 1f;

    [Range(2, 15f)]
    [SerializeField]
    public float rotationSpeed = 5f;

    public Transform defaultPosition;
    public Transform leftPosition;
    public Transform rightPosition;
    public Transform downPosition;
    public Transform frontPosition;


    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;
    KeyCode down = KeyCode.S;
    KeyCode forward = KeyCode.W;



    void Update()
    {
        if (Input.GetKey(left))
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPosition.position, duckSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, leftPosition.rotation, duckSpeed * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(down))
        {
            transform.position = Vector3.MoveTowards(transform.position, downPosition.position, duckSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, downPosition.rotation, duckSpeed * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(right))
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPosition.position, duckSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rightPosition.rotation, duckSpeed * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(forward))
        {
            transform.position = Vector3.MoveTowards(transform.position, frontPosition.position, duckSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, frontPosition.rotation, duckSpeed * rotationSpeed * Time.deltaTime);
        }
        else
        {
            //Return to default
            transform.position = Vector3.MoveTowards(transform.position, defaultPosition.position, duckSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, defaultPosition.rotation, duckSpeed * rotationSpeed * Time.deltaTime);

        }
    }
}

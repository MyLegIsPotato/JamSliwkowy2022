using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ObjectRotator : MonoBehaviour
{
    public float speed = 100;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(speed * Time.deltaTime, speed * 0.7f * Time.deltaTime, speed* 0.9f * Time.deltaTime), Space.Self);
    }
}

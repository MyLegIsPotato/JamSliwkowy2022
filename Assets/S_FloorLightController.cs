using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FloorLightController : MonoBehaviour
{
    void Start()
    {
        S_ElevatorController.OnElevatorArrived += (x) => { if(x == GetComponent<S_FloorNumber>().thisFloorNum) SetLights(true); }; 
        S_ElevatorController.OnElevatorDeparted += (x) => { if (x == GetComponent<S_FloorNumber>().thisFloorNum) SetLights(false); };
    }

    void SetLights(bool enabled)
    {
        print("Turning on lights on level " + this.name);

        Light[] lights = GetComponentsInChildren<Light>(true);

        print("There are " + lights.Length + " lights.");


        foreach (Light item in lights)
        {
            item.gameObject.SetActive(enabled);
        }
    }
}

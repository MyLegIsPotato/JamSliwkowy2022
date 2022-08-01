using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FloorLightController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        S_ElevatorController.OnElevatorArrived += () => { SetLights(true); }; 
        S_ElevatorController.OnElevatorDeparted += () => { SetLights(false); };

    }

    void SetLights(bool enabled)
    {
        Light[] lights = GetComponentsInChildren<Light>();

        foreach (var item in lights)
        {
            item.enabled = enabled;
        }
    }
}

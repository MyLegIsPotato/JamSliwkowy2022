using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FloorLightController : MonoBehaviour
{
    public float delay = 0;

    private void OnDisable()
    {

    }

    void Awake()
    {
        S_ElevatorController.OnElevatorArrived += (x) => { if(x == GetComponent<S_FloorNumber>().thisFloorNum) StartCoroutine(SetLights(true)); }; 
        S_ElevatorController.OnElevatorDeparted += (x) => { if (x == GetComponent<S_FloorNumber>().thisFloorNum) StartCoroutine(SetLights(false)); };
    }

    IEnumerator SetLights(bool enabled)
    {
        print("Turning on lights on level " + this.name);

        yield return new WaitForSeconds(delay);

        Light[] lights = GetComponentsInChildren<Light>(true);

        print("There are " + lights.Length + " lights.");


        foreach (Light item in lights)
        {
            item.gameObject.SetActive(enabled);
        }

        yield return null;
    }
}

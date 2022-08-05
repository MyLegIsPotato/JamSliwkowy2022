using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class S_ButtonDelay : MonoBehaviour
{
    public float delay = 3;

    private void OnEnable()
    {
        StartCoroutine(InteractableAfter());
    }

    IEnumerator InteractableAfter()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<UnityEngine.UI.Button>().interactable = true;
        yield return null;
    }
}

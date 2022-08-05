using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StartScreenAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalZ(this.gameObject, 0.8f, 10f);
    }
}

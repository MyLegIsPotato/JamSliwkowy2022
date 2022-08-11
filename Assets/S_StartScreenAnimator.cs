using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_StartScreenAnimator : MonoBehaviour
{
    [SerializeField]
    bool isRestartButton = false;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalZ(this.gameObject, 0.8f, 10f);
    }

    private void OnMouseDown()
    {
        print("klik w gui");
        if (isRestartButton)
        {
           
            S_GameManager.RestartGame();
            
        }
        else if (!S_GameManager.gameStarted )
        {
            GetComponent<BoxCollider>().enabled = false;
            FindObjectOfType<S_GameManager>().StartGame();
           
        }
     
    }
}

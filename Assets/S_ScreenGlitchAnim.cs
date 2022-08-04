using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScreenGlitchAnim : MonoBehaviour
{    
    public void PlayGlitchEffect()
    {
        FindObjectOfType<S_PlayerManager>().GlitchScreen();
        FindObjectOfType<S_PlayerManager>().CrackScreen();
    }
}

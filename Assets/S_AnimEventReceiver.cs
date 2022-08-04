using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AnimEventReceiver : MonoBehaviour
{    
    public void PlayGlitchEffect()
    {
        FindObjectOfType<S_PlayerManager>().GlitchScreen();
        FindObjectOfType<S_PlayerManager>().CrackScreen();
    }

    public void ZombieAttack()
    {
        GetComponentInParent<S_Enemy_Zombie>().Attack();
    }

    public void ZombieTaunt()
    {
        GetComponentInParent<S_Enemy_Zombie>().ScreamOnce();
    }
}

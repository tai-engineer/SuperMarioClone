using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosionEffect : MonoBehaviour
{
    // Used by aniamtion event
    public void DestroyEffect()
    {
        Destroy(gameObject);
    }
}

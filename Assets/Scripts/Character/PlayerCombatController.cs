using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public Transform shotPointRight;
    public Transform shotPointLeft;
    public float timeBetweenShots = 0.1f;
    public GameObject fireBall;
    [HideInInspector]
    public bool isShooting = false;

    public void StartShootingFireBall()
    {
        isShooting = true;
    }
    public void FinishShootingFireBall()
    {
        isShooting = false;
    }
    public void ShootFireBall(bool rightPoint)
    {
        Vector3 position = rightPoint ? shotPointRight.position : shotPointLeft.position;
        Quaternion rotation = rightPoint ? shotPointRight.rotation : shotPointLeft.rotation;
        Instantiate(fireBall, position, rotation);
    }
}

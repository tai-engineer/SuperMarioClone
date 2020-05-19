using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMushroom : Mushroom
{
    protected override void Awake()
    {
        base.Awake();
        Type = PowerUpType.SUPER;
    }

    void FixedUpdate()
    {
        MoveUpward(1f);
        Patrol();
    }

}

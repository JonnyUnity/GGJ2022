using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : EnemyControllerBase
{

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        IsAttacking = true;
        
    }

}

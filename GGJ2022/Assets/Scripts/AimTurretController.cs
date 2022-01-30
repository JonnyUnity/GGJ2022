using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurretController : EnemyControllerBase
{

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector2 direction = DirectionToTarget();

        IsAttacking = true;

        //float rotZ = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        float rotZ = Vector2.SignedAngle(direction, transform.up);
        Debug.Log(rotZ);

        if (-45f <= rotZ && rotZ <= 45)
        {
            OnRotateEvent.Invoke(direction);
        }        
        
    }

}

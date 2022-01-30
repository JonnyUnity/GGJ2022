using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderEnemy : EnemyControllerBase
{

    [SerializeField] private float _followRange = 15f;
    [SerializeField] private float _shootRange = 2f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        IsAttacking = false;

        if (distance <= _followRange)
        {
            OnRotateEvent.Invoke(direction);
            OnMoveEvent.Invoke(direction);

            if (distance <= _shootRange)
            {
                IsAttacking = true;
            }
        }
        else
        {
            OnMoveEvent.Invoke(Vector2.zero);
        }
        

    }


}

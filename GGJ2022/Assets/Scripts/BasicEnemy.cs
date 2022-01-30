using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyControllerBase
{
    [SerializeField] private float _followRange = 15f;
    [SerializeField] private float _shootRange = 10f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        IsAttacking = false;
        if (distance <= _followRange)
        {
            if (distance <= _shootRange)
            {
                // close enough to shoot, so stop and shoot!
                OnRotateEvent.Invoke(direction);
                OnMoveEvent.Invoke(Vector2.zero);
                IsAttacking = true;
            }
            else
            {
                // can see player so move in to shooting rage.
                OnMoveEvent.Invoke(direction);
            }
        }
        else
        {
            // too far from player, don't move.
            OnMoveEvent.Invoke(Vector2.zero);
        }

    }


}

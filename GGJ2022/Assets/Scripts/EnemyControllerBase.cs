using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyControllerBase : TopDownCharacterController
{
    [SerializeField] private string _targetTag = "Player";
    private Transform _target;

    protected string TargetTag => TargetTag;
    protected Transform Target => _target;


    protected override void Awake()
    {
        base.Awake();

        _target = FindClosestTarget();
    }

    protected virtual void FixedUpdate()
    {
        _target = FindClosestTarget();
    }


    private Transform FindClosestTarget()
    {
        return GameObject.FindGameObjectsWithTag(_targetTag)
            .OrderBy(o => Vector3.Distance(o.transform.position, transform.position))
            .First().transform;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, Target.transform.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (Target.transform.position - transform.position).normalized;
    }


}

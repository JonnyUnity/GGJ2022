using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TopDownCharacterController : MonoBehaviour
{
    public bool IsAttacking { get; set; }
    private float _timeSinceLastAttack;

    private float _attackDelay;

    public Vector2 LastMovementDirection;
    public Vector2 LastPosition;
    public Quaternion LastRotation;

    // events
    private UnityEvent<Vector2> onMoveEvent = new UnityEvent<Vector2>();
    private UnityEvent onStopMovingEvent = new UnityEvent();
    private UnityEvent onAttackEvent = new UnityEvent();
    private UnityEvent<Vector2> onRotateEvent = new UnityEvent<Vector2>();

    public UnityEvent<Vector2> OnMoveEvent => onMoveEvent;
    public UnityEvent OnStopMovingEvent => onStopMovingEvent;
    public UnityEvent OnAttackEvent => onAttackEvent;
    public UnityEvent<Vector2> OnRotateEvent => onRotateEvent;

    protected virtual void Awake()
    {
        
    }

    void Start()
    {
        
    }


    protected virtual void Update()
    {

        _timeSinceLastAttack += Time.deltaTime;

        if (IsAttacking && _timeSinceLastAttack > _attackDelay)
        {
            _timeSinceLastAttack = 0f;
            onAttackEvent.Invoke();
        }

    }
}

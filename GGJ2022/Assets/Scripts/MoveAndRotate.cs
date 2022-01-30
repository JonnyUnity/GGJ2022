using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndRotate : MonoBehaviour
{

    private TopDownCharacterController _controller;
    private StatsHandler _stats;
    private Rigidbody2D _rigidBody;
    private Transform _transform;

    protected Vector2 _movementDirection = Vector2.zero;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _stats = GetComponent<StatsHandler>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        AddListeners();
    }


    public void AddListeners()
    {
        _controller.OnMoveEvent.AddListener(Move);
        _controller.OnStopMovingEvent.AddListener(StopMoving);
        _controller.OnRotateEvent.AddListener(OnAim);
    }

    public void RemoveListeners()
    {
        _controller.OnMoveEvent.RemoveListener(Move);
        _controller.OnStopMovingEvent.RemoveListener(StopMoving);
        _controller.OnRotateEvent.RemoveListener(OnAim);
    }

    private void Move(Vector2 direction)
    {
        //Debug.Log("Move: " + direction);
        _movementDirection = direction;
    }

    private void StopMoving()
    {
        _movementDirection = Vector2.zero;
    }

    private void OnAim(Vector2 aimDirection)
    {
        _transform.up = aimDirection;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {

        Vector2 movement = _movementDirection * _stats.Stats.Speed * Time.deltaTime;

        _rigidBody.MovePosition(_rigidBody.position + movement);

    }
}

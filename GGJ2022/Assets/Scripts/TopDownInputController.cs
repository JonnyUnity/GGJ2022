using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownInputController : TopDownCharacterController
{
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }


    public void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>().normalized;
        LastMovementDirection = move;
        OnMoveEvent.Invoke(move);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        if (!(newAim.normalized == newAim))
        {
            Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
            newAim = (worldPos - (Vector2)transform.position).normalized;
        }

        if (newAim.magnitude >= .9f)
        {
            OnRotateEvent.Invoke(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

}

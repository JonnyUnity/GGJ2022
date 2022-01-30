using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{

    [SerializeField] private Transform _turretPivot;

    private AimTurretController _controller;


    private void Awake()
    {
        _controller = GetComponent<AimTurretController>();
    }

    void Start()
    {
        _controller.OnRotateEvent.AddListener(OnAim);
    }


    private void OnAim(Vector2 aimDirection)
    {

        float rotZ = Vector2.SignedAngle(transform.up, aimDirection);


        _turretPivot.rotation = Quaternion.Euler(0, 0, rotZ);


    }

}

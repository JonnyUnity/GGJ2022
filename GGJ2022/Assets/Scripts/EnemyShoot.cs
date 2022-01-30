using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletSpawn;
    [SerializeField] private float _bulletSpeed;

    private Rigidbody2D _rigidBody;
    private TopDownCharacterController _controller;
    private StatsHandler _stats;

    private Vector2 _aimDirection;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _stats = GetComponent<StatsHandler>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _aimDirection = gameObject.transform.up;

    }

    private void Start()
    {
        _controller.OnAttackEvent.AddListener(OnShoot);
        _controller.OnRotateEvent.AddListener(OnAim);
    }

    public void OnAim(Vector2 direction)
    {
        _aimDirection = direction;
    }


    public void OnShoot()
    {
        var bulletObj = Instantiate(_bulletPrefab);

        bulletObj.transform.position = _bulletSpawn.transform.position;
        var rotateVector = _aimDirection.normalized;

        if (bulletObj.TryGetComponent(out Bullet bullet))
        {
            bullet.Init(rotateVector, _bulletSpeed, _stats.Stats.ProjectileDamage);
        }

    }



}

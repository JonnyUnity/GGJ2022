using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject[] _bulletSpawns;
    [SerializeField] private float _bulletSpeed;


    private TopDownCharacterController _controller;
    private StatsHandler _stats;
    private HealthSystem _health;

    private Vector2 _aimDirection;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _stats = GetComponent<StatsHandler>();
        _health = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        _controller.OnAttackEvent.AddListener(OnExplode);
        _controller.OnRotateEvent.AddListener(OnAim);
    }

    private void OnAim(Vector2 direction)
    {
        _aimDirection = direction;
    }

    private void OnExplode()
    {
        
        foreach (GameObject spawn in _bulletSpawns)
        {
            var bulletObj = Instantiate(_bulletPrefab);
            bulletObj.transform.position = spawn.transform.position;

            if (bulletObj.TryGetComponent(out Bullet bullet))
            {
                bullet.Init(spawn.transform.up, _bulletSpeed, _stats.Stats.ProjectileDamage);
            }

        }

        _health.OnDeath.Invoke("");

    }


}

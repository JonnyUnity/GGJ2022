using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _bulletSpeed;

    [SerializeField] private GameObject _bulletSpawn;

    private float SecondsBetweenShots = 2f;
    private float _timeSinceLastShot;

    private Rigidbody2D _rigidBody;
    private BoxCollider2D _collider;
    private Transform _transform;

    private float _minRangeSquared;

    private void Awake()
    {
        _transform = transform;
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _minRangeSquared = _minDistance * _minDistance;
    }

    void Start()
    {
        
    }


    void Update()
    {

        // move towards player...

        //var heading = GameManager.Instance.PlayerTransform.position - _transform.position;

        //_transform.up = heading.normalized;

        //var movement = heading.normalized * _moveSpeed * Time.deltaTime;

        //if (heading.sqrMagnitude > _minRangeSquared)
        //{
        //    //Debug.Log("Moving Enemy", this);
        //    _rigidBody.MovePosition(_transform.position + movement);
        //}

        //_transform.LookAt(GameManager.Instance.PlayerTransform);

        //_timeSinceLastShot += Time.deltaTime;
        //if (_timeSinceLastShot >= SecondsBetweenShots)
        //{
        //    Shoot();
        //    _timeSinceLastShot = 0;
        //}


    }

    //private void Shoot()
    //{

    //    var spawnPos = _bulletSpawn.transform.position + (_bulletSpawn.transform.forward * 10);

    //    //var bulletObj = Instantiate(_bulletPrefab, spawnPos, _bulletSpawn.transform.localRotation);
    //    var bulletObj = Instantiate(_bulletPrefab, spawnPos, transform.rotation);
    //    //bulletObj.transform.parent = null;
    //    //bulletObj.transform.forward = GameManager.Instance.PlayerTransform.position;
    //    Debug.Log("BULLET SPAWNER ROTATION: " + _bulletSpawn.transform.rotation);

    //    Debug.Log("BULLET UP: " + bulletObj.transform.up);

    //    if (bulletObj.TryGetComponent(out Bullet bullet))
    //    {
    //        bullet.Fire(_bulletSpeed);
    //    }


    //}

}

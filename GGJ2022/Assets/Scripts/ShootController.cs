using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletSpawn;
    [SerializeField] private float _bulletSpeed;

    private float SecondsBetweenShots = 2f;
    private float _timeSinceLastShot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        _timeSinceLastShot += Time.deltaTime;
        if (_timeSinceLastShot >= SecondsBetweenShots)
        {
            Shoot();
            _timeSinceLastShot = 0;
        }

    }

    private void Shoot()
    {

        var spawnPos = _bulletSpawn.transform.position + (_bulletSpawn.transform.forward * 10);

        //var bulletObj = Instantiate(_bulletPrefab, spawnPos, _bulletSpawn.transform.localRotation);
        var bulletObj = Instantiate(_bulletPrefab, spawnPos, transform.rotation);
        //bulletObj.transform.parent = null;
        //bulletObj.transform.forward = GameManager.Instance.PlayerTransform.position;
        //Debug.Log("BULLET SPAWNER ROTATION: " + _bulletSpawn.transform.rotation);

        //Debug.Log("BULLET UP: " + bulletObj.transform.up);

        if (bulletObj.TryGetComponent(out Bullet bullet))
        {
            bullet.Fire(_bulletSpeed);
        }


    }

}

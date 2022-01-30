using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform _transform;
    private Rigidbody2D _rigidBody;

    private Vector2 _direction;
    private float _speed;

    private int _damage;

    private bool isReady;

    private void Awake()
    {
        _transform = transform;
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    public void Init(Vector2 direction, float speed, int damage)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
        isReady = true;
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        _rigidBody.velocity = _direction * _speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;

        if (obj.CompareTag("Player"))
        {
            if (obj.TryGetComponent(out PlayerController playerController))
            {
                if (!playerController.IsRolling)
                {
                    // do hit!
                    if (obj.TryGetComponent(out HealthSystem health))
                    {
                        health.ChangeHealth(-_damage);
                    }

                    Destroy(this.gameObject);
                }

            }
        }
        else if (obj.CompareTag("Shield"))
        {

            ShieldController shield = obj.GetComponentInParent<ShieldController>();
            if (shield != null)
            {
                if (shield.IsHeld)
                {
                    Destroy(this.gameObject);
                }
            }
            

            //if (obj.TryGetComponentInParent(out ShieldController shield))
            //{
            //    if (shield.IsHeld)
            //    {
            //        Destroy(this.gameObject);
            //    }
            //}
        }
        else if (!obj.CompareTag("Bullet"))
        {
            //Debug.Log(obj.name);
            Destroy(this.gameObject);
        }

    }
}

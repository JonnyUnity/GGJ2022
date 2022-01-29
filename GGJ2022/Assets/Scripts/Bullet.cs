using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform _transform;
    private Rigidbody2D _rigidBody;

    public int Damage = 10;

    private void Awake()
    {
        _transform = transform;
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    public void Fire(float bulletSpeed)
    {
        _rigidBody.AddForce(_transform.up * bulletSpeed);
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
                    Destroy(this.gameObject);
                }

            }
        }
        else
        {
            Destroy(this.gameObject);
        }

        //Debug.Log(collision.gameObject.name);
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{

    private float _damage;

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;

        if (!obj.CompareTag("Player"))
        {

            if (obj.TryGetComponent(out HealthSystem health))
            {
                health.ChangeHealth(-_damage);
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var obj = collision.gameObject;

        if (!obj.CompareTag("Player"))
        {

            if (obj.TryGetComponent(out HealthSystem health))
            {
                health.ChangeHealth(-_damage);
            }
        }
    }

}

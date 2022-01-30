using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPlayerInPit(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckPlayerInPit(collision);
    }


    private void CheckPlayerInPit(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCenter"))
        {

            var obj = collision.gameObject;
            var playerObj = obj.transform.parent.gameObject;

            if (playerObj.TryGetComponent(out PlayerController playerController))
            {
                if (!playerController.IsRolling)
                {
                    HealthSystem health = obj.GetComponentInParent<HealthSystem>();

                    if (health != null)
                    {
                        health.FallInPit();
                    }
                }
            }
        }
    }


}

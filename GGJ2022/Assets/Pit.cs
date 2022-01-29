using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var obj = collision.gameObject;
            if (obj.TryGetComponent(out PlayerController playerController))
            {
                yield return playerController.FallInPit();
            }
        }
    }

    

}

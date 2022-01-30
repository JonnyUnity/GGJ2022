using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private int _triggerIndex;
    
    private bool triggered;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            var obj = collision.gameObject;

            if (obj.CompareTag("Player"))
            {

                GameManager.Instance.UpdateSpawnIndex(_triggerIndex);

                triggered = true;


            }
        }
    }

}

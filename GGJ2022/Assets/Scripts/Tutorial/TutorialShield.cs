using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShield : MonoBehaviour
{

    [SerializeField] private PlayerController _playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerController.PickUpShield();
        gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTutorial : MonoBehaviour
{
    [SerializeField] private int _tutorialLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.UpdateTutorial(_tutorialLevel);
    }

}

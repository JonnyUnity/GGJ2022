using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField] private float delay;

    public void OnDeath()
    {
        // do other things...

        // death animation

        // particles, sound effects...



        //Destroy(gameObject, 2f); // destroy after X seconds.

        Destroy(gameObject, delay);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : TopDownCharacterController
{

    private void FixedUpdate()
    {
        IsAttacking = true;   
    }

}

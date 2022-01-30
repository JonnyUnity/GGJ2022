using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerController : PlayerController
{

    private bool IsRollEnabled;

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void DoAction()
    {
        _controller.IsAttacking = false;

        if (IsHoldingShield)
        {
            ThrowShield();
        }
        else if (!IsRolling && IsRollEnabled)
        {
            StartCoroutine(CR_PerformRoll());
        }
    }

}

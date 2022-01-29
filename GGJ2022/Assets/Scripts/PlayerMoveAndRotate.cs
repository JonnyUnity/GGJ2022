using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAndRotate : MoveAndRotate
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    //protected override void FixedUpdate()
    //{
    //    if (!_playerController.IsRolling)
    //    {
    //        base.FixedUpdate();
    //    }
    //}

}

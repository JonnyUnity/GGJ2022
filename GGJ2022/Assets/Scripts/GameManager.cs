using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private ShieldController Shield;


    public Transform PlayerTransform
    {
        get
        {
            return Player.transform;
        }
    }

    public bool IsShieldHeld = true;


    void Start()
    {
        
    }


    void Update()
    {
        
    }


    public void ThrowShield()
    {
        
        Shield.Throw();
        IsShieldHeld = false;

    }

    public void PickUpShield()
    {
        Shield.PickUp(Player.transform);
        IsShieldHeld = true;
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{

    public CharacterStats Stats;

    public void UpdateSpeed(float newSpeed)
    {
        Stats.Speed = newSpeed;
    }


}

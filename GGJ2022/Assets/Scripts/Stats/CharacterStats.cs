using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "GGJ2022/New Character Stats", order = 0)]
public class CharacterStats : ScriptableObject
{
    public float MaxHealth;
    public float Speed;
    public float TimeBetweenAttacks;
    public int ProjectileDamage;
    public float AccuracyVariance;
}

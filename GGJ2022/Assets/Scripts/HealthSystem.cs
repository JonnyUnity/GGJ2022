using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{

    private StatsHandler _stats;


    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent<string> onDeath = new UnityEvent<string>();


    public UnityEvent OnDamage => onDamage;
    public UnityEvent<string> OnDeath => onDeath;


    public float MaxHealth;
    public float CurrentHealth;

    private void Awake()
    {
        _stats = GetComponent<StatsHandler>();
    }

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        MaxHealth = _stats.Stats.MaxHealth;
        CurrentHealth = _stats.Stats.MaxHealth;
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0)
        {
            return false;
        }

        CurrentHealth += change;
        //CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
        //CurrentHealth = Mathf.Max(0, CurrentHealth);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        OnDamage.Invoke();

        if (CurrentHealth == 0f)
        {
            OnDeath.Invoke("HealthZero");
        }

        return true;

    }

    public void FallInPit()
    {

        OnDeath.Invoke("Pit");
    }


}

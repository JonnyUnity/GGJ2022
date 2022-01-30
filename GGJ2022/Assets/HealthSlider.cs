using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Slider _healthslider;

    private HealthSystem _health;

    private void Awake()
    {
        _health = _player.GetComponent<HealthSystem>();

    }

    // Start is called before the first frame update
    void Start()
    {
        _health.OnDamage.AddListener(UpdateHealth);
        _health.OnInit.AddListener(UpdateHealth);
    }

    private void UpdateHealth()
    {
        _healthslider.value = _health.CurrentHealth;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    protected TopDownInputController _controller;
    private ShieldController _shield;
    private MoveAndRotate _moveRotate;
    private StatsHandler _stats;
    private HealthSystem _health;
    private PlayerInput _playerInput;

    private Transform _transform;

    [SerializeField] private Transform _thrownSpawn;

    [SerializeField] private float _withShieldSpeed = 2f;
    [SerializeField] private float _withoutShieldSpeed = 4f;
    [SerializeField] private float _rollSpeed = 4.5f;



    private Rigidbody2D _rigidBody;
    private SpriteRenderer _renderer;
    private Color _origColour;
    private BoxCollider2D _collider;



    public bool IsHoldingShield = true;
    public bool CanThrow;
    public bool CanRoll;
    public bool IsRolling;
    private float RollTime = 0.7f;


    [SerializeField] private UnityEvent onPlayerDied = new UnityEvent();
    public UnityEvent OnPlayerDied => onPlayerDied;

    private void Awake()
    {
        _controller = GetComponent<TopDownInputController>();
        _moveRotate = GetComponent<MoveAndRotate>();
        _stats = GetComponent<StatsHandler>();
        _rigidBody = GetComponent<Rigidbody2D>();
        //_renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _shield = GetComponent<ShieldController>();
        _health = GetComponent<HealthSystem>();

        _playerInput = GetComponent<PlayerInput>();

        //_origColour = _renderer.color;
        _transform = transform;        
    }

    protected virtual void Start()
    {
        _controller.OnAttackEvent.AddListener(DoAction);
        _shield.SetShieldDamage(_stats.Stats.ProjectileDamage);

        _health.OnDeath.AddListener(PlayerDied);
    }


    public void PlayerDied(string deathType)
    {
        _playerInput.DeactivateInput();

        StartCoroutine(CR_ShowPlayerDeath(deathType));


        // throw event up the chain to the game manager (is this good?)
        //onPlayerDied.Invoke();
    }



    private IEnumerator CR_ShowPlayerDeath(string deathType)
    {
        Debug.Log("Start death!");
        //_rigidBody.Sleep();

        if (deathType == "Pit")
        {
            // shrink sprite...
            // _transform.localScale = Vector3.Lerp(_transform.localScale, Vector3.zero, 1 / (Time.deltaTime * 2f));
            yield return StartCoroutine(CR_FallInPit());
        }
        else
        {
            // play death animination
        }

        //yield return new WaitForSeconds(2f);

        OnPlayerDied.Invoke();

    }

    private IEnumerator CR_FallInPit()
    {
        var elapsed = 0f;

        while (elapsed < 1f)
        {
            //_transform.localScale = Vector3.Lerp(_transform.localScale, Vector3.zero, 1 / elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }

    }

    private IEnumerator CR_DeathAnimation()
    {
        yield return new WaitForSeconds(2f);
    }

    protected virtual void DoAction()
    {

        _controller.IsAttacking = false;

        if (CanThrow && IsHoldingShield)
        {
            ThrowShield();
        }
        else if (CanRoll && !IsRolling)
        {
            StartCoroutine(CR_PerformRoll());
        }

        
    }

    public void InitValues(int tutorial)
    {
        _health.ResetHealth();

        if (!_playerInput.inputIsActive)
        {
            _playerInput.ActivateInput();
        }

        IsHoldingShield = (tutorial > 0);
        CanThrow = (tutorial >= 1);
        CanRoll = (tutorial >= 2);

        if (tutorial == 0)
        {
            _shield.Remove();
            _stats.UpdateSpeed(_withoutShieldSpeed);
        }
        else
        {
            _shield.PickUp(_transform);
            _stats.UpdateSpeed(_withShieldSpeed);
        }

    }


    protected void ThrowShield()
    {
        _shield.Throw(_transform.up.normalized, _thrownSpawn.localPosition);


        _stats.UpdateSpeed(_withoutShieldSpeed);
        IsHoldingShield = false;
    }


    public void PickUpShield()
    {
        _shield.PickUp(_transform);

        _stats.UpdateSpeed(_withShieldSpeed);
        IsHoldingShield = true;
    }


    public IEnumerator FallInPit()
    {

        _moveRotate.RemoveListeners();
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        _controller.OnStopMovingEvent.Invoke();

        yield return StartCoroutine(FallIntoPit());

        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _moveRotate.AddListeners();

    }


    private IEnumerator FallIntoPit()
    {


        // play fall animation?
        yield return new WaitForSeconds(3f);

        //Debug.Log(_controller.LastPosition);
        _rigidBody.position = _controller.LastPosition;

    }


    private void Update()
    {
        if (!IsRolling)
        {
            _controller.LastPosition = transform.position;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Shield"))
        {
            if (_shield.CanBePickedUp())
            {
                PickUpShield();
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Shield"))
        {
            if (_shield.CanBePickedUp())
            {
                PickUpShield();
            }
        }
    }


    protected IEnumerator CR_PerformRoll()
    {
        var currentDirection = (Vector3)_controller.LastMovementDirection.normalized;

        if (currentDirection != Vector3.zero)
        {
            IsRolling = true;
            //_renderer.material.color = Color.red;

            var elapsedTime = 0f;
            while (elapsedTime < RollTime)
            {
                _transform.position += currentDirection * _rollSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            //_renderer.material.color = _origColour;
            IsRolling = false;
        }

        
    }


}
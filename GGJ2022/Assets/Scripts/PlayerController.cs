using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private TopDownCharacterController _controller;
    private MoveAndRotate _moveRotate;
    private StatsHandler _stats;
    private Transform _transform;

    [SerializeField] private float _withShieldSpeed = 2f;
    [SerializeField] private float _withoutShieldSpeed = 4f;
    [SerializeField] private float _rollSpeed = 4.5f;
    //private float currentSpeed;


    private Rigidbody2D _rigidBody;
    private SpriteRenderer _renderer;
    private Color _origColour;
    private CircleCollider2D _collider;

    //private Vector2 _leftStickInput;

    public bool IsRolling;
    private float RollTime = 0.7f;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _moveRotate = GetComponent<MoveAndRotate>();
        _stats = GetComponent<StatsHandler>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
        _origColour = _renderer.color;
        _transform = transform;        
    }

    void Start()
    {
        _controller.OnAttackEvent.AddListener(DoAction);
    }

    private void DoAction()
    {
        Debug.Log("DO ACTION!");
        _controller.IsAttacking = false;

        if (GameManager.Instance.IsShieldHeld)
        {
            GameManager.Instance.ThrowShield();
            _stats.UpdateSpeed(_withoutShieldSpeed);
        }
        else if (!IsRolling)
        {
            StartCoroutine(CR_PerformRoll());
        }


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
        Debug.Log("FELL INTO A PIT!");

        // play fall animation?
        yield return new WaitForSeconds(3f);

        Debug.Log(_controller.LastPosition);
        //transform.position = _controller.LastPosition - _controller.LastMovementDirection.normalized;
        //transform.position = _controller.LastPosition;
        _rigidBody.position = _controller.LastPosition;

    }


    private void Update()
    {
        if (!IsRolling)
        {
            _controller.LastPosition = transform.position;
        }
    }

    //protected override void Update()
    //{
    //    if (IsAttacking)
    //    {
    //        if (GameManager.Instance.IsShieldHeld)
    //        {
    //            GameManager.Instance.ThrowShield();
    //            _stats.UpdateSpeed(_withoutShieldSpeed);
    //        }
    //        else if (!IsRolling)
    //        {
    //            StartCoroutine(CR_PerformRoll());
    //        }
    //    }
    //}

    private void GetPlayerInput()
    {

        if (IsRolling)
        {
            return;
        }
        
       


        //_leftStickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //if (_leftStickInput != Vector2.zero)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsShieldHeld)
        //    {
        //        StartCoroutine(CR_PerformRoll());
        //    }
        //}

        //if (GameManager.Instance.IsShieldHeld)
        //{
        //    // Throw the shield when the mouse button is clicked.
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        GameManager.Instance.ThrowShield();
        //        currentSpeed = _withoutShieldSpeed;

        //    }
        //}
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield"))
        {
            GameManager.Instance.PickUpShield();
            _stats.UpdateSpeed(_withShieldSpeed);
        }
    }

    private IEnumerator CR_PerformRoll()
    {
        var currentDirection = (Vector3)_controller.LastMovementDirection.normalized;

        if (currentDirection != Vector3.zero)
        {
            IsRolling = true;
            _renderer.material.color = Color.red;

            var elapsedTime = 0f;
            while (elapsedTime < RollTime)
            {
                _transform.position += currentDirection * _rollSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _renderer.material.color = _origColour;
            IsRolling = false;
        }

        
    }


}

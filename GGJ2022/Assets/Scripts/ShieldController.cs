using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    [SerializeField] private Sprite _heldSprite;
    [SerializeField] private Sprite _thrownSprite;

    private Transform _transform;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _collider;

    public bool IsHeld = true;
    private bool IsStationary;

    private void Awake()
    {
        _transform = transform;
        _renderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    public void Start()
    {
        
        
    }

    public void PickUp(Transform playerTransform)
    {
        
        _rigidBody.isKinematic = true;
        _transform.localScale = new Vector3(1, 0.25f);
        _collider.size = _transform.localScale;
        _collider.isTrigger = false;
        _renderer.sprite = _heldSprite;

        _transform.parent = playerTransform;
        _transform.localRotation = Quaternion.identity;
        _transform.localPosition = new Vector3(0, 0.87f, 0); // change this to constant!
        IsHeld = true;
        IsStationary = false;

    }

    private void ReadyToPickUp()
    {
        _collider.isTrigger = true;
        IsStationary = true;
    }

    public void Update()
    {

        if (IsHeld || IsStationary)
        {
            return;
        }

        //if (IsHeld)
        //{
        //    // Throw the shield when the mouse button is clicked.
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Throw();
        //        Debug.Log("Throwing Shield");
        //        _rigidBody.isKinematic = false;
        //        IsHeld = false;
        //        _renderer.sprite = _thrownSprite;


        //    }
        //}
        
        var shieldSpeed = _rigidBody.velocity.magnitude;
        //Debug.Log(shieldSpeed);
        if (shieldSpeed < 0.5)
        {
            Debug.Log("Shield stopped, ready to pickup");
            _rigidBody.velocity = new Vector2(0, 0);
            ReadyToPickUp();
        }

    }

    public void Throw()
    {
        _transform.parent = null;
        _rigidBody.isKinematic = false;
        _transform.localScale = new Vector3(1, 1);
        _collider.size = _transform.localScale;
        _renderer.sprite = _thrownSprite;

        Vector2 force = new Vector2(0, 10);
        _rigidBody.AddRelativeForce(force, ForceMode2D.Impulse);

        IsHeld = false;

    }

}
